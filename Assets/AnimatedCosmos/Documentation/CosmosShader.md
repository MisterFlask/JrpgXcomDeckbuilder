# __Cosmos skybox shader documentation__

__The package contains two shaders:__
- Cosmos animated (Skybox/Cosmos Animated)
- Cosmos static (Skybox/Cosmos Static)

## __Cosmos Animated__ shader
### Properties:
- ***Color*** - tint color
- ***Main Texture*** - Main skybox texture (Cubemap)
- ***Colorize*** - saturation of skybox (0 is BW, 1 is Colorful)
<br><br>
- ***Detail textures*** - Additive 2D detail textures
- ***Intensity*** - Influence of texture (0 is no influence)
- ***Scale*** - Scale of detail texture
- ***Distortion*** - Distortion of detail texture
- ***Speed*** - Animation speed

## __Cosmos Static__ shader
### It is same as __Cosmos Animated__ shader but without animation. Also more lightweight and good for mobile apps