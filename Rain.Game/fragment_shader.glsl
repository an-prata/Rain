#version 330 core
out vec4 color;
  
in vec4 fragmentColor; // the input variable from the vertex shader (same name and same type)  

void main()
{
    color = fragmentColor;
} 