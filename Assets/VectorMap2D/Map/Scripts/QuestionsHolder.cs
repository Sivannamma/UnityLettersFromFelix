using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionsHolder
{

    // question.Add(new KeyValuePair<string, string>(currentLine[0], "\"" + currentLine[1] + "\""));
    public List<KeyValuePair<string, string>> questions()
    {
        List<KeyValuePair<string, string>> q = new List<KeyValuePair<string, string>>();
        q.Add(new KeyValuePair<string, string>("the country with second largest population in the world", "\"" + "India" + "\""));
        q.Add(new KeyValuePair<string, string>("the country with the largest population in the world", "\"" + "China" + "\""));
        q.Add(new KeyValuePair<string, string>("the country which corona virus came from", "\"" + "China" + "\""));
        q.Add(new KeyValuePair<string, string>("the country which \"Harry Truman\" was it's president", "\"" + "United States" + "\""));
        q.Add(new KeyValuePair<string, string>("the country which \"Franklin D.Roosevelt\" was it's president", "\"" + "United States" + "\""));
        q.Add(new KeyValuePair<string, string>("the country which \"Joseph Stalin\" was it's president", "\"" + "Russia" + "\""));
        q.Add(new KeyValuePair<string, string>("the country which \"Benito Mussolini\" was it's leader", "\"" + "Italy" + "\""));
        q.Add(new KeyValuePair<string, string>("the couuntry which \"Winston Churchill was it's prime minister", "\"" + "United Kingdom" + "\""));
        q.Add(new KeyValuePair<string, string>("the country that sent man into the moon for the first time", "\"" + "United States" + "\""));
        q.Add(new KeyValuePair<string, string>("the country who sent man into space for first time", "\"" + "Russia" + "\""));
        q.Add(new KeyValuePair<string, string>("the country who was boombed by atomic bomb", "\"" + "Japan" + "\""));
        q.Add(new KeyValuePair<string, string>("the country with the most olympic medals", "\"" + "United States" + "\""));
        q.Add(new KeyValuePair<string, string>("the country which started the second world war", "\"" + "Germany" + "\""));
        q.Add(new KeyValuePair<string, string>("the country which started the first world war", "\"" + "Germany" + "\""));
        q.Add(new KeyValuePair<string, string>("the country which the \"Disk on key\" was invented", "\"" + "Israel" + "\""));
        q.Add(new KeyValuePair<string, string>("the country with famous big wall", "\"" + "China" + "\""));
        q.Add(new KeyValuePair<string, string>("the country where the dead-sea is located", "\"" + "Israel" + "\""));
        q.Add(new KeyValuePair<string, string>("the country with the famous colosseum", "\"" + "Italy" + "\""));
        q.Add(new KeyValuePair<string, string>("where was the smallest town in the world", "\"" + "Croatia" + "\""));
        q.Add(new KeyValuePair<string, string>("the country with the highest life expectancy", "\"" + "Japan" + "\""));
        q.Add(new KeyValuePair<string, string>("the country which the \"Drakula figure\" was invented", "\"" + "Romania" + "\""));
        q.Add(new KeyValuePair<string, string>("the longest country in the world", "\"" + "Chile" + "\""));
        q.Add(new KeyValuePair<string, string>("the country which has the \"Vatican City\"", "\"" + "Italy" + "\""));
        q.Add(new KeyValuePair<string, string>("the country where \"William Shakespeare\" was born", "\"" + "United Kingdom" + "\""));
        q.Add(new KeyValuePair<string, string>("the country where the author \"J.K.Rowling\" was born", "\"" + "United Kingdom" + "\""));
        q.Add(new KeyValuePair<string, string>("the country with the largest economy in Europe continent", "\"" + "Germany" + "\""));
        q.Add(new KeyValuePair<string, string>("the country with the second largest economy in Europe continent", "\"" + "France" + "\""));
        q.Add(new KeyValuePair<string, string>("the country in south america which has borders with two oceans", "\"" + "Colombia" + "\""));
        q.Add(new KeyValuePair<string, string>("the country with most football world cup titles", "\"" + "Brazil" + "\""));
        q.Add(new KeyValuePair<string, string>("the largest country in Europe continent", "\"" + "Ukraine" + "\""));
        q.Add(new KeyValuePair<string, string>("the youngest country in the world", "\"" + "S. Sudan" + "\""));
        q.Add(new KeyValuePair<string, string>("the coldest country in the world", "\"" + "Antarctica" + "\""));
        q.Add(new KeyValuePair<string, string>("the second coldest country in the world", "\"" + "Russia" + "\""));
        q.Add(new KeyValuePair<string, string>("the third coldest country in the world", "\"" + "Canada" + "\""));
        q.Add(new KeyValuePair<string, string>("the most hottest country in the world(1991-2020)", "\"" + "Mali" + "\""));
        q.Add(new KeyValuePair<string, string>("the country with the world's longest road tunnel", "\"" + "Norway" + "\""));
        q.Add(new KeyValuePair<string, string>("the country where the Nobel Peace Prize is awarded", "\"" + "Norway" + "\""));
        q.Add(new KeyValuePair<string, string>("the country that invented nicotine replacement gum", "\"" + "Sweden" + "\""));
        q.Add(new KeyValuePair<string, string>("the country with the highest number of patents in Europe(due to 2021)", "\"" + "Sweden" + "\""));
        q.Add(new KeyValuePair<string, string>("the world's happiest country", "\"" + "Finland" + "\""));
        q.Add(new KeyValuePair<string, string>("the country with the largest ice cave in the world", "\"" + "Austria" + "\""));
        q.Add(new KeyValuePair<string, string>("the country where \"Arnold Schwarzenegger\" was born", "\"" + "Austria" + "\""));
        q.Add(new KeyValuePair<string, string>("the country where \"Red Bull\" energy drink was invented", "\"" + "Austria" + "\""));
        q.Add(new KeyValuePair<string, string>("where Ferdinand Porsche, the founder of the sports car company Porsche, was born", "\"" + "Austria" + "\""));
        q.Add(new KeyValuePair<string, string>("the country where you can marry a dead person", "\"" + "France" + "\""));
        q.Add(new KeyValuePair<string, string>("the country with the oldest flag in the world", "\"" + "Denmark" + "\""));
        q.Add(new KeyValuePair<string, string>("the country which is home to the world�s biggest castle", "\"" + "Poland" + "\""));
        q.Add(new KeyValuePair<string, string>("the country which Europe�s heaviest animals live in", "\"" + "Poland" + "\""));
        q.Add(new KeyValuePair<string, string>("the country where the famous \"Santa Claus\" was born in", "\"" + "Turkey" + "\""));
        q.Add(new KeyValuePair<string, string>("the country with the wettest inhabited place on Earth", "\"" + "India" + "\""));
        q.Add(new KeyValuePair<string, string>("the country with the highest population of vegetarians", "\"" + "India" + "\""));
        q.Add(new KeyValuePair<string, string>("the country that also known as \"The Babel Tower of the World\"", "\"" + "Chad" + "\""));
        q.Add(new KeyValuePair<string, string>("the country that was part of France until 1960", "\"" + "Chad" + "\""));
        q.Add(new KeyValuePair<string, string>("the country that it's flag was based on France flag", "\"" + "Chad" + "\""));
        q.Add(new KeyValuePair<string, string>("the largest country in Africa", "\"" + "Algeria" + "\""));
        q.Add(new KeyValuePair<string, string>("the country which nearly 30 percent of it is National Parks", "\"" + "Tanzania" + "\""));
        q.Add(new KeyValuePair<string, string>("the country that has the tallest mountain in Africa", "\"" + "Tanzania" + "\""));
        q.Add(new KeyValuePair<string, string>("where is the lowest place at Africa", "\"" + "Ethiopia" + "\""));
        q.Add(new KeyValuePair<string, string>("where is the largest capital city in Africa", "\"" + "Ethiopia" + "\""));
        q.Add(new KeyValuePair<string, string>("where is the oldest university", "\"" + "Morocco" + "\""));
        q.Add(new KeyValuePair<string, string>("the country that has a city painted in blue color all over", "\"" + "Morocco" + "\""));
        q.Add(new KeyValuePair<string, string>("the country with the largest economy in Africa", "\"" + "Nigeria" + "\""));
        q.Add(new KeyValuePair<string, string>("where was the largest diamond was found", "\"" + "South Africa" + "\""));
        q.Add(new KeyValuePair<string, string>("the country which also called \"The rainbow nation\"", "\"" + "South Africa" + "\""));
        q.Add(new KeyValuePair<string, string>("the country which has 3 capital cities", "\"" + "South Africa" + "\""));
        q.Add(new KeyValuePair<string, string>("the country that dismantle their nuclear weapons program", "\"" + "South Africa" + "\""));
        q.Add(new KeyValuePair<string, string>("where is the largest waterfall", "\"" + "Zimbabwe" + "\""));
        q.Add(new KeyValuePair<string, string>("the country where 90% of the population live on the coast", "\"" + "Australia" + "\""));
        q.Add(new KeyValuePair<string, string>("the country that have mustered the most Miss World winners", "\"" + "Venezuela" + "\""));
        q.Add(new KeyValuePair<string, string>("the country which has the point on Earth closest to the sun", "\"" + "Ecuador" + "\""));
        q.Add(new KeyValuePair<string, string>("where is the highest official capital city", "\"" + "Ecuador" + "\""));
        q.Add(new KeyValuePair<string, string>("where the potato originated at", "\"" + "Peru" + "\""));
        q.Add(new KeyValuePair<string, string>("where is the world's largest flying bird at", "\"" + "Peru" + "\""));
        q.Add(new KeyValuePair<string, string>("where is the world's highest lake", "\"" + "Peru" + "\""));
        q.Add(new KeyValuePair<string, string>("the country with second largest population in the world", "\"" + "India" + "\""));
        q.Add(new KeyValuePair<string, string>("the country that generates more than 99% of its electricity using renewable energy", "\"" + "Costa Rica" + "\""));
        q.Add(new KeyValuePair<string, string>("the country which has 5 active volcanoes", "\"" + "Costa Rica" + "\""));
        q.Add(new KeyValuePair<string, string>("the smallest country in central america", "\"" + "El Salvador" + "\""));
        q.Add(new KeyValuePair<string, string>("the only country in Central America without a Caribbean coastline", "\"" + "El Salvador" + "\""));
        q.Add(new KeyValuePair<string, string>("the country which experienced a bloody civil war from 1979 to 1992", "\"" + "El Salvador" + "\""));
        q.Add(new KeyValuePair<string, string>("the country which also known as \"Land of Volcanoes\"", "\"" + "El Salvador" + "\""));
        q.Add(new KeyValuePair<string, string>("the country which locals also known as \"guanacos\"", "\"" + "El Salvador" + "\""));
        q.Add(new KeyValuePair<string, string>("the country where you can see the sun rise on the Pacific and set on the Atlantic", "\"" + "Panama" + "\""));
        q.Add(new KeyValuePair<string, string>("the first latin america country to adopt the U.S. currency as its own", "\"" + "Panama" + "\""));
        q.Add(new KeyValuePair<string, string>("the country with the smallest population in central america", "\"" + "Panama" + "\""));
        q.Add(new KeyValuePair<string, string>("the country which is home to approximately 3000 islands", "\"" + "Greece" + "\""));
        q.Add(new KeyValuePair<string, string>("the country that 80% of it is made up of mountains", "\"" + "Greece" + "\""));
        q.Add(new KeyValuePair<string, string>("the country which it's capital is named after the goddess Athena", "\"" + "Greece" + "\""));
        q.Add(new KeyValuePair<string, string>("the country that has more archaeological museums than any country", "\"" + "Greece" + "\""));
        q.Add(new KeyValuePair<string, string>("the country which it's capital has more theatres than any other city in the world", "\"" + "Greece" + "\""));
        q.Add(new KeyValuePair<string, string>("the country which became independent from the British in 1960", "\"" + "Cyprus" + "\""));
        q.Add(new KeyValuePair<string, string>("the country where taxi drivers in do not give change", "\"" + "Cyprus" + "\""));
        q.Add(new KeyValuePair<string, string>("the country which it's shape is like a Cigar pipe(in the map)", "\"" + "Cyprus" + "\""));
        q.Add(new KeyValuePair<string, string>("the country that is the first to include its map on the flag", "\"" + "Cyprus" + "\""));
        q.Add(new KeyValuePair<string, string>("the country that it's air force developed the first air-dropped bomb", "\"" + "Bulgaria" + "\""));
        q.Add(new KeyValuePair<string, string>("the country with the highest population density in Europe", "\"" + "Netherlands" + "\""));
        q.Add(new KeyValuePair<string, string>("the country that invented the first stock market in the world", "\"" + "Netherlands" + "\""));
        q.Add(new KeyValuePair<string, string>("the country where there are more bicycles in than people", "\"" + "Netherlands" + "\""));
        q.Add(new KeyValuePair<string, string>("the country with the tallest people in the world", "\"" + "Netherlands" + "\""));
        q.Add(new KeyValuePair<string, string>("the first country in the world to legalize same-sex marriage", "\"" + "Netherlands" + "\""));
        q.Add(new KeyValuePair<string, string>("the country which it's national symbol is peeing boy", "\"" + "Belgium" + "\""));
        q.Add(new KeyValuePair<string, string>("the country where the famous comic smurfs came from", "\"" + "Belgium" + "\""));
        q.Add(new KeyValuePair<string, string>("the country where the world wide web(www) was invented", "\"" + "Belgium" + "\""));
        q.Add(new KeyValuePair<string, string>("the country where the saxophone was invented", "\"" + "Belgium" + "\""));
        q.Add(new KeyValuePair<string, string>("the second country in the world to have bigger female ratio than men", "\"" + "Estonia" + "\""));
        q.Add(new KeyValuePair<string, string>("the first country in the world to adopt online voting", "\"" + "Estonia" + "\""));
        q.Add(new KeyValuePair<string, string>("the country which it's nick name is \"text capital of the world\"", "\"" + "Philippines" + "\""));
        q.Add(new KeyValuePair<string, string>("the country which is home to approximately 7000 islands", "\"" + "Philippines" + "\""));
        q.Add(new KeyValuePair<string, string>("where the world�s first animated feature film was produced", "\"" + "Argentina" + "\""));
        q.Add(new KeyValuePair<string, string>("the country that is known as the home of Tango", "\"" + "Argentina" + "\""));
        q.Add(new KeyValuePair<string, string>("the country that the remains of the largest known dinosaur were discovered", "\"" + "Argentina" + "\""));
        q.Add(new KeyValuePair<string, string>("the country that went through five presidents in just 10 days", "\"" + "Argentina" + "\""));
        q.Add(new KeyValuePair<string, string>("the country that had two female presidents", "\"" + "Argentina" + "\""));
        q.Add(new KeyValuePair<string, string>("the country which is named after Venezuelan leader Simon Bolivar", "\"" + "Bolivia" + "\""));
        q.Add(new KeyValuePair<string, string>("the country which has 2 different voting ages", "\"" + "Bolivia" + "\""));
        q.Add(new KeyValuePair<string, string>("the country that is the cheapest to visit in South America", "\"" + "Bolivia" + "\""));
        q.Add(new KeyValuePair<string, string>("the country which every single house has its own unique name", "\"" + "Uruguay" + "\""));
        q.Add(new KeyValuePair<string, string>("the country which has the largest navy in the world", "\"" + "China" + "\""));
        q.Add(new KeyValuePair<string, string>("the country which dueling is legal", "\"" + "Paraguay" + "\""));
        q.Add(new KeyValuePair<string, string>("the country who has the longest nantional anthem", "\"" + "Uruguay" + "\""));
        q.Add(new KeyValuePair<string, string>("the country where this game was invented", "\"" + "Israel" + "\""));
        q.Add(new KeyValuePair<string, string>("the country which holds the largest pyramid in the world", "\"" + "Mexico" + "\""));
        q.Add(new KeyValuePair<string, string>("the country with the largest number of taxi cabs in the world", "\"" + "Mexico" + "\""));
        q.Add(new KeyValuePair<string, string>("the country which the same meteorite that wiped-out dinosaurs struck it", "\"" + "Mexico" + "\""));
        q.Add(new KeyValuePair<string, string>("the country which Color TV was invented by", "\"" + "Mexico" + "\""));
        q.Add(new KeyValuePair<string, string>("the country which it's currency name based on local bird", "\"" + "Guatemala" + "\""));
        q.Add(new KeyValuePair<string, string>("the country that it's name means:\"land of many trees\"", "\"" + "Guatemala" + "\""));
        q.Add(new KeyValuePair<string, string>("the country which it's civil war was the longest in the Latin American history", "\"" + "Guatemala" + "\""));
        q.Add(new KeyValuePair<string, string>("the country which also called \"great depths\"", "\"" + "Honduras" + "\""));
        q.Add(new KeyValuePair<string, string>("the first country to ban smoking in your own home", "\"" + "Honduras" + "\""));
        q.Add(new KeyValuePair<string, string>("the country which it's nick name is \"Banana Republic\"", "\"" + "Honduras" + "\""));
        q.Add(new KeyValuePair<string, string>("the country which the national anthem plays on the radio every morning", "\"" + "Costa Rica" + "\""));
        q.Add(new KeyValuePair<string, string>("the country which native people call themselves ticos and ticas", "\"" + "Costa Rica" + "\""));
        return q;
    }
}
