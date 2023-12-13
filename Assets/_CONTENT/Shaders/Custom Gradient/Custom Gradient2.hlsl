 
// Custom Gradient
 
void CustomGradient_float(float inputValue,
                   float4 color1,
                   float4 color2,
                   float4 color3,
                   float4 color4,
                   float4 color5,
                   float4 color6,
                   float4 color7,
                   float4 color8,
                   float location1,
                   float location2,
                   float location3,
                   float location4,
                   float location5,
                   float location6,
                   float location7,
                   float location8,
                   out float4 outFloat)
{
    if(inputValue<location1)
    {
        outFloat = color1;
    }
    else if(inputValue<location2)
    {
        float pos = (inputValue-location1)/(location2-location1);
        outFloat = color2;
    }
    else if(inputValue<location3)
    {
        float pos = (inputValue-location2)/(location3-location2);
        outFloat = color3;
    }
    else if(inputValue<location4)
    {
        float pos = (inputValue-location3)/(location4-location3);
        outFloat = color4;
    }
    else if(inputValue<location5)
    {
        float pos = (inputValue-location4)/(location5-location4);
        outFloat = color5;
    }
    else if(inputValue<location6)
    {
        float pos = (inputValue-location5)/(location6-location5);
        outFloat = color6;
    }
    else if(inputValue<location7)
    {
        float pos = (inputValue-location6)/(location7-location6);
        outFloat = color7;
    }
	else if(inputValue<location8)
    {
        float pos = (inputValue-location7)/(location8-location7);
        outFloat = color8;
    }
    else
    {
        outFloat = color8;
    }
}