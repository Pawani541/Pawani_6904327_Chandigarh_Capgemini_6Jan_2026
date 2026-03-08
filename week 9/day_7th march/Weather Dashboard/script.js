function checkWeather()
{
    var city = document.getElementById("city").value;
    var result = document.getElementById("result");
    var body = document.getElementById("body");

    var weather;
    var temp;

    if(city.toLowerCase() == "paris")
    {
        weather = "Sunny";
        temp = "22°C";
    }
    else if(city.toLowerCase() == "london")
    {
        weather = "Rain";
        temp = "16°C";
    }
    else if(city.toLowerCase() == "tokyo")
    {
        weather = "Cloudy";
        temp = "20°C";
    }
    else
    {
        weather = "Sunny";
        temp = "25°C";
    }

    result.innerHTML = "City: " + city + "<br>Temp: " + temp + "<br>Weather: " + weather;

    if(weather == "Sunny")
    {
        body.style.background = "yellow";
    }
    else if(weather == "Rain")
    {
        body.style.background = "lightblue";
    }
    else
    {
        body.style.background = "gray";
    }
}