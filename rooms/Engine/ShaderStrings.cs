namespace Engine
{
    public static class ShaderStrings
    {
        public static string[] directional_vertex_shader()
        {
            return new string[]
            {
                "precision lowp float;",

                "mat3 inverse(mat3 m)",
                "{",
                    "mat3 adj;",
                    "adj[0][0] = + (m[1][1] * m[2][2] - m[2][1] * m[1][2]);",
                    "adj[1][0] = - (m[1][0] * m[2][2] - m[2][0] * m[1][2]);",
                    "adj[2][0] = + (m[1][0] * m[2][1] - m[2][0] * m[1][1]);",
                    "adj[0][1] = - (m[0][1] * m[2][2] - m[2][1] * m[0][2]);",
                    "adj[1][1] = + (m[0][0] * m[2][2] - m[2][0] * m[0][2]);",
                    "adj[2][1] = - (m[0][0] * m[2][1] - m[2][0] * m[0][1]);",
                    "adj[0][2] = + (m[0][1] * m[1][2] - m[1][1] * m[0][2]);",
                    "adj[1][2] = - (m[0][0] * m[1][2] - m[1][0] * m[0][2]);",
                    "adj[2][2] = + (m[0][0] * m[1][1] - m[1][0] * m[0][1]);",

                    "float det = (+ m[0][0] * (m[1][1] * m[2][2] - m[1][2] * m[2][1])",
                            "- m[0][1] * (m[1][0] * m[2][2] - m[1][2] * m[2][0])",
                            "+ m[0][2] * (m[1][0] * m[2][1] - m[1][1] * m[2][0]));",
                    "return adj / det;",
                "}",

                "mat3 transpose(mat3 inMatrix)",
                "{",
                    "vec3 i0 = inMatrix[0];",
                    "vec3 i1 = inMatrix[1];",
                    "vec3 i2 = inMatrix[2];",

                    "mat3 outMatrix = mat3(",
                        "vec3(i0.x, i1.x, i2.x),",
                        "vec3(i0.y, i1.y, i2.y),",
                        "vec3(i0.z, i1.z, i2.z));",
                    "return outMatrix;",
                "}",

                "attribute vec3 vPos;",
                "attribute vec3 vColor;",
                "attribute vec3 vNormal;",

                "varying vec3 fPos;",
                "varying vec3 fColor;",
                "varying vec3 fNormal;",

                "uniform mat4 modelview;",
                "uniform mat4 view;",
                "uniform mat4 model;",

                "void main(){",

                    "gl_Position = modelview * vec4(vPos, 1.0);",
                    "fColor = vColor;",

                    "mat3 m = mat3(model[0].xyz, model[1].xyz, model[2].xyz);",
                    "mat3 n = inverse(m);",
                    "mat3 normMatrix = transpose(n);",

                    "fNormal = normMatrix * vNormal;",
                    "fPos = (model * vec4(vPos, 1.0)).xyz;",
                "}"
            };
        }

        public static string[] directional_fragment_shader()
        {
            return new string[]
            {
                "precision lowp float;",
                /*
                "mat4 inverse(mat4 m)",
                "{",
                    "float SubFactor00 = m[2][2] * m[3][3] - m[3][2] * m[2][3];",
                    "float SubFactor01 = m[2][1] * m[3][3] - m[3][1] * m[2][3];",
                    "float SubFactor02 = m[2][1] * m[3][2] - m[3][1] * m[2][2];",
                    "float SubFactor03 = m[2][0] * m[3][3] - m[3][0] * m[2][3];",
                    "float SubFactor04 = m[2][0] * m[3][2] - m[3][0] * m[2][2];",
                    "float SubFactor05 = m[2][0] * m[3][1] - m[3][0] * m[2][1];",
                    "float SubFactor06 = m[1][2] * m[3][3] - m[3][2] * m[1][3];",
                    "float SubFactor07 = m[1][1] * m[3][3] - m[3][1] * m[1][3];",
                    "float SubFactor08 = m[1][1] * m[3][2] - m[3][1] * m[1][2];",
                    "float SubFactor09 = m[1][0] * m[3][3] - m[3][0] * m[1][3];",
                    "float SubFactor10 = m[1][0] * m[3][2] - m[3][0] * m[1][2];",
                    "float SubFactor11 = m[1][1] * m[3][3] - m[3][1] * m[1][3];",
                    "float SubFactor12 = m[1][0] * m[3][1] - m[3][0] * m[1][1];",
                    "float SubFactor13 = m[1][2] * m[2][3] - m[2][2] * m[1][3];",
                    "float SubFactor14 = m[1][1] * m[2][3] - m[2][1] * m[1][3];",
                    "float SubFactor15 = m[1][1] * m[2][2] - m[2][1] * m[1][2];",
                    "float SubFactor16 = m[1][0] * m[2][3] - m[2][0] * m[1][3];",
                    "float SubFactor17 = m[1][0] * m[2][2] - m[2][0] * m[1][2];",
                    "float SubFactor18 = m[1][0] * m[2][1] - m[2][0] * m[1][1];",
                    "mat4 adj;",
                    "adj[0][0] = + (m[1][1] * SubFactor00 - m[1][2] * SubFactor01 + m[1][3] * SubFactor02);",
                    "adj[1][0] = - (m[1][0] * SubFactor00 - m[1][2] * SubFactor03 + m[1][3] * SubFactor04);",
                    "adj[2][0] = + (m[1][0] * SubFactor01 - m[1][1] * SubFactor03 + m[1][3] * SubFactor05);",
                    "adj[3][0] = - (m[1][0] * SubFactor02 - m[1][1] * SubFactor04 + m[1][2] * SubFactor05);",
                    "adj[0][1] = - (m[0][1] * SubFactor00 - m[0][2] * SubFactor01 + m[0][3] * SubFactor02);",
                    "adj[1][1] = + (m[0][0] * SubFactor00 - m[0][2] * SubFactor03 + m[0][3] * SubFactor04);",
                    "adj[2][1] = - (m[0][0] * SubFactor01 - m[0][1] * SubFactor03 + m[0][3] * SubFactor05);",
                    "adj[3][1] = + (m[0][0] * SubFactor02 - m[0][1] * SubFactor04 + m[0][2] * SubFactor05);",
                    "adj[0][2] = + (m[0][1] * SubFactor06 - m[0][2] * SubFactor07 + m[0][3] * SubFactor08);",
                    "adj[1][2] = - (m[0][0] * SubFactor06 - m[0][2] * SubFactor09 + m[0][3] * SubFactor10);",
                    "adj[2][2] = + (m[0][0] * SubFactor11 - m[0][1] * SubFactor09 + m[0][3] * SubFactor12);",
                    "adj[3][2] = - (m[0][0] * SubFactor08 - m[0][1] * SubFactor10 + m[0][2] * SubFactor12);",
                    "adj[0][3] = - (m[0][1] * SubFactor13 - m[0][2] * SubFactor14 + m[0][3] * SubFactor15);",
                    "adj[1][3] = + (m[0][0] * SubFactor13 - m[0][2] * SubFactor16 + m[0][3] * SubFactor17);",
                    "adj[2][3] = - (m[0][0] * SubFactor14 - m[0][1] * SubFactor16 + m[0][3] * SubFactor18);",
                    "adj[3][3] = + (m[0][0] * SubFactor15 - m[0][1] * SubFactor17 + m[0][2] * SubFactor18);",
                    "float det = (+ m[0][0] * adj[0][0]",
                            "+ m[0][1] * adj[1][0]",
                            "+ m[0][2] * adj[2][0]",
                            "+ m[0][3] * adj[3][0]);",
                    "return adj / det;",
                "}",
                */

                "varying vec3 fPos;",
                "varying vec3 fColor;",
                "varying vec3 fNormal;",

                "vec4 outputColor;",
                "uniform mat4 view;",

                "vec3 materialAmbient = vec3(0.2, 0.2, 0.2);",
                "vec3 materialDiffuse = vec3(0.9, 0.9, 0.9);",
                "vec3 materialSpecular = vec3(0.1, 0.1, 0.1);",
                "float materialSpecExpo = 0.9;",

                "uniform vec3 lightDir;",
                "uniform vec3 lightColor;",
                "uniform float lightAmbientIntens;",
                "uniform float lightDiffuseIntens;",


                "void main(){",

                    "vec4 color = vec4(fColor, 1.0);",
                    "outputColor = vec4(0, 0, 0, 1);",

                    "vec3 n = normalize(fNormal);",

                    "vec3 lightVec = normalize(lightDir);",

                    "vec4 lightAmbient = lightAmbientIntens * vec4(lightColor, 0.0);",
                    "vec4 lightDiffuse = lightDiffuseIntens * vec4(lightColor, 0.0);",

                    "outputColor = outputColor + color * lightAmbient * vec4(materialAmbient, 0.0);",
                    "float lambertMaterialDiffuse = max(dot(n, lightVec), 0.0);",
                    "outputColor = outputColor + (lightDiffuse * color * vec4(materialDiffuse, 0.0)) * lambertMaterialDiffuse;",

                    "vec3 reflectionVec = normalize(reflect(-lightVec, fNormal));",
                    "vec3 viewVec = normalize(vec3(view) - fPos);",

                    "float materialSpecularReflection = max(dot(fNormal, lightVec), 0.0) * pow(max(dot(reflectionVec, viewVec), 0.0), materialSpecExpo);",

                    "outputColor = outputColor + vec4(materialSpecular * lightColor, 0.0) * materialSpecularReflection;",
                    "gl_FragColor = outputColor;",
                "}"
            };
        }
    }
}