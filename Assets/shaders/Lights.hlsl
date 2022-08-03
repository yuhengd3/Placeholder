#ifndef CUSTOM_LIGHTING_INCLUDED
#define CUSTOM_LIGHTING_INCLUDED

#ifndef SHADERGRAPH_PREVIEW
    #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/ShaderPass.hlsl"
    #if (SHADERPASS != SHADERPASS_FORWARD)
        #undef REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR
    #endif
#endif

struct CustomLightingData{
    float3 normalWS;
    float3 viewDirWS;
    float3 albedo;
    float smoothness;
    float4 shadowCoord;
    float3 posWS;

    float shadeOffset;
    float specularRange;
    float contourOffset;

    float ambientOcclusion;
    float3 bakedGI;
};

float GetSmoothnessPower(float raw){
    return exp2(10 * raw + 1);
}


#ifndef SHADERGRAPH_PREVIEW
float3 CustomGlobalIllumination(CustomLightingData d){
    float3 indirectDiffuse = d.albedo * d.bakedGI * d.ambientOcclusion;

    float3 reflectVector = reflect(-d.viewDirWS, d.normalWS);
    float fresnel = Pow4(1 - saturate(dot(d.viewDirWS, d.normalWS)));
    float3 indirectSpecular = GlossyEnvironmentReflection(reflectVector,
        RoughnessToPerceptualRoughness(1-d.smoothness),
        d.ambientOcclusion) * fresnel;

    return indirectDiffuse + indirectSpecular;
}

float3 CustomLightHandling(CustomLightingData d, Light light){

    float3 radiance = light.color * (light.shadowAttenuation * light.distanceAttenuation);

    float _diffuse = ceil(dot(d.normalWS, light.direction));
    float specularDot = dot(d.normalWS, light.direction) - d.specularRange;
    float specular = saturate(normalize(specularDot));
    float _contour = dot(d.normalWS, d.viewDirWS) - d.contourOffset;
    float contour = ceil(saturate(1 - _contour) * radiance);
    float diffuse = _diffuse + d.shadeOffset;

    float3 color = (d.albedo * diffuse + contour) * radiance + specular;

    return color;
}
#endif 


float3 CalculateCustomLighting(CustomLightingData d){
#ifdef SHADERGRAPH_PREVIEW
    float3 lightDir = float3(0.5,0.5,0);
    float intensity = saturate(dot(d.normalWS, lightDir)) + pow(saturate(dot(d.normalWS, normalize(d.viewDirWS + lightDir))), GetSmoothnessPower(d.smoothness));
    return d.albedo * intensity;
#else
    Light mainLight = GetMainLight(d.shadowCoord, d.posWS, 1);
    MixRealtimeAndBakedGI(mainLight, d.normalWS, d.bakedGI);
    float3 color = CustomGlobalIllumination(d);
    color += CustomLightHandling(d, mainLight);

    #ifdef _ADDITIONAL_LIGHTS
        uint numAdditionalLights = GetAdditionalLightsCount();
        for (uint lightI = 0; lightI < numAdditionalLights; lightI++){
            Light light = GetAdditionalLight(lightI, d.posWS, 1);
            color += CustomLightHandling(d, light);
        }
    #endif
    return color;
#endif
}

void CalculateCustomLighting_float(float3 Normal, float3 Albedo, float3 ViewDirection, float Smoothness, float3 Position, float AmbientOcclusion, float2 LightmapUV,
    float shadeOffset, float specularRange, float cOffset,
    out float3 Color){

    CustomLightingData d;
    d.posWS = Position;
    d.albedo = Albedo;
    d.normalWS = Normal;
    d.viewDirWS = ViewDirection;
    d.smoothness = Smoothness;
    d.ambientOcclusion = AmbientOcclusion;
    d.shadeOffset = shadeOffset;
    d.specularRange = specularRange;
    d.contourOffset = cOffset;

    #ifdef SHADERGRAPH_PREVIEW
        d.shadowCoord = 0;
        d.bakedGI = 0;
    #else

    
        float4 positionCS = TransformWorldToHClip(Position);
        #if SHADOWS_SCREEN
            d.shadowCoord = ComputeScreenPos(positionCS);
        #else
            d.shadowCoord = TransformWorldToShadowCoord(Position);
        #endif

        float3 lightmapUV;
        OUTPUT_LIGHTMAP_UV(LightmapUV, unity_LightmapST, lightmapUV);
        float3 vertexSH;
        OUTPUT_SH(Normal, vertexSH);
        d.bakedGI = SAMPLE_GI(lightmapUV, vertexSH, Normal);
    #endif

    Color = CalculateCustomLighting(d);
}

#endif