/// <reference path="navbarfunc.js" />
// https://api.themoviedb.org/3/search/tv?api_key=1c107f2bd2f3fc2aee24aa4f2f8d8647&language=en-US&page=1&include_adult=false&query=Grey%27s%20Anatomy


$(document).ready(function () {
    $("#getTV").click(getTV);
    $("#tvShowName").keyup(function (e) {
        if (e.keyCode == 13) {
            getTV()
        }
    });

    key = "90f77ef6862d870eb9f5fff3bc587100";

    url = "https://api.themoviedb.org/";
    imagePath = "https://image.tmdb.org/t/p/w500/";
    // 64467
    // 1416

    //https://api.themoviedb.org/3/tv/1416/season/0/episode/64467?api_key=1c107f2bd2f3fc2aee24aa4f2f8d8647&language=en-US
    //AIzaSyBAFQpdpUo7xpd6xQKspMv7Ci-Ex5xmbDg
    navBarVisability();

    if (localStorage.series != null) {
     //   getTV();
        var series = JSON.parse(localStorage["series"]);
        $("#tvShowName").val(series.seriesObj.Name);
        toGetTv(series.seriesObj.Name);
    }

    document.getElementById('card').onclick = function () {
        document.getElementById('scripted').focus();
    };

        //$("i").click(function () {
        //    $("i,span").toggleClass("press", 1000);
        //});

    swal({
        title: "Sweet!",
        text: "Here's a custom image.",
        imageUrl: 'thumbs-up.jpg'
    });
   
    swal("Sweet!", "Here's a custom image.", 'thumbs-up.jpg');

});

//https://api.themoviedb.org/3/tv/{tv_id}/season/{season_number}?api_key=<<api_key>>&language=en-US
function aboutPage() {
    localStorage.setItem("series", JSON.stringify(totalSeries));
    setTimeout(function () { location.href = 'aboutTv.html'; }, 2000);
}

function getTV() {
  
    let name = $("#tvShowName").val();
    $("#seasonsList").html("");
    $("#episodeList").html("");
    toGetTv(name);
}

function toGetTv(name) {
    i = 1;
    k = 0;
    let method = "3/search/tv?";
    let api_key = "api_key=" + key;
    let moreParams = "&language=en-US&page=1&include_adult=false&";
    let query = "query=" + encodeURIComponent(name);
    let apiCall = url + method + api_key + moreParams + query;
    ajaxCall("GET", apiCall, "", getTVSuccessCB, getTVErrorCB);
}

//Show the TVSohw 
function getTVSuccessCB(tv) {
    buildTvSeriese(tv);
    console.log(tv);
    $("#Episodes").html("");
    seasonsList = "";
    tvId = tv.results[0].id;
    posterURL = tv.results[0].poster_path
    let poster = imagePath + posterURL;
    str = "<img src='" + poster + "'/>";
    let stars = 5;
    let popularity = tv.results[0].popularity;
    switch (true) {
        case (popularity < 40):
            stars = 1
            break;
        case (popularity < 60):
            stars = 2
            break;
        case (popularity < 200):
            stars = 3
            break;
        case (popularity < 400):
            stars = 4
            break;
    }
    str += "<img id='starsPopularity' src= '../images/" + stars + "stars.png'/><button id='aboutBtn' onclick='aboutPage()'>TV SHOW about</button>";
    $("#ph").html(str);

    let method = "3/tv/";
    let api_key = "api_key=" + key;

    let apiCall = url + method + tvId + "/season/" + i + "?" + api_key;
    ajaxCall("GET", apiCall, "", getSeasonSuccessCB, getSeasonErrorCB);
}

//create obj for sql table - in button "add" we send it to the sql table
seriesObj = null;
totalSeries = null;
function buildTvSeriese(tv) {
    console.log(tv);
    seriesObj = {
        Id: tv.results[0].id,
        First_air_date: tv.results[0].first_air_date,
        Name: tv.results[0].name,
        Origin_country: tv.results[0].origin_country[0],
        Original_language: tv.results[0].original_language,
        Overview: tv.results[0].overview,
        Popularity: tv.results[0].popularity,
        Poster_path: imagePath + tv.results[0].poster_path
    }
    extras = {

        Backdrop_path: imagePath + tv.results[0].backdrop_path,
        Genre_ids: tv.results[0].genre_ids

    }
    totalSeries = {
        seriesObj,
        extras
    }
    console.log(totalSeries);

  //  localStorage.setItem("series", JSON.stringify(totalSeries));
}
function getTVErrorCB(err) {
    console.log(err);
}

seasonsCount = 0;
function getSeasonSuccessCB(season) {
    console.log(season);
    epArr = [];
    if (season.poster_path == null)
        season.poster_path = posterURL;
    seasonsList += "<div id= '" + i + "' class='card' onclick=showEpisode(this.id)>";
    seasonsList += "<img id= 'imgInCard' src='" + imagePath + season.poster_path + "'style='width:100%'>";
    seasonsList += "<h4 style='text-align:center'><b>" + season.name + "</b></h4></div>";

    i++;
    seasonsCount++;

    $("#seasonsList").html(seasonsList);
    let method = "3/tv/";
    let api_key = "api_key=" + key;

    let apiCall = url + method + tvId + "/season/" + i + "?" + api_key;
    ajaxCall("GET", apiCall, "", getSeasonSuccessCB, getSeasonErrorCB);
}


function getSeasonErrorCB(err) {
    if (err.status == 404) {
        $("#seasonsList").html(seasonsList);
        console.log(err);
    }
}

function showEpisode(seasonNum) {
    chooseSeasonClass(seasonNum);
    j = 1;
    saveSeasonNum = seasonNum;
    episodesList = "";
    $("#Episodes").html(episodesList);
    let method = "3/tv/";
    let api_key = "api_key=" + key;

    apiCall = url + method + tvId + "/season/" + seasonNum + "/episode/" + j + "?" + api_key;
    ajaxCall("GET", apiCall, "", getEpisodeSuccessCB, getEpisodeErrorCB);
}

function chooseSeasonClass(seasonNum) {
    document.getElementById(seasonNum).classList.add("cardSelected");
    for (var i = 1; i < seasonsCount; i++) {
        if (i != seasonNum) {
            document.getElementById(i).classList.remove("cardSelected");
        }
    }
}

//Get Episodes from the TMDB server
c = 0;
episode = null;
function getEpisodeSuccessCB(episodes) {
    episode = {
        EpisodeId: episodes.id,
        SeriesId: seriesObj.Id,//foreign key
        SeriesName: seriesObj.Name,
        SeasonNum: episodes.season_number,
        EpisodeName: episodes.name,
        ImageURL: imagePath + episodes.still_path,
        Overview: episodes.overview,
        AirDate: episodes.air_date
    }
    if (episodes.still_path == null)
        episode.ImageURL = imagePath + posterURL;
      

    epArr.push(episode);    //מערך של כל הפרקים
    episodesList += "<div class='card2'><img class= 'imgCard' id='" + j + "' src='" + episode.ImageURL + "'>"; //td changed to div
    episodesList += "<div class='episodeBlock'><br><b class='episodeTitle'>" + (episodes.name).slice(0, 17);
    episodesList += "</b></br> " + episodes.air_date + "</br></br><div id='episodeOverView'>" + episodes.overview + "</div></div>";
    if (localStorage.user != undefined) {
        episodesList += "</br><button class='addBtn' id='" + c + "' type='button' onclick=PostToServer(epArr[this.id])> Add </button> </center>";
        //episodesList += `<div id="` + c + `" onclick=PostToServer(epArr[this.id])>
        //                      <i></i>
        //                      <span>liked!</span>
        //                 </div></center>`
    }
    episodesList += "</div>";
    c++;

    $("#Episodes").html(episodesList);
    j++;
    let method = "3/tv/";
    let api_key = "api_key=" + key;
    let apiCall = url + method + tvId + "/season/" + saveSeasonNum + "/episode/" + j + "?" + api_key;
    ajaxCall("GET", apiCall, "", getEpisodeSuccessCB, getEpisodeErrorCB);
}

function getEpisodeErrorCB(err) {
    c = 0;
    console.log(err);
}
totalObj = {};// for corrent inserted alert
function PostToServer(episodeToAdd) {
    let api = "../api/Totals";
    //add new object for DB
    console.log(episodeToAdd);
    totalObj = {
        Series: seriesObj,
        Episode: episodeToAdd,
        UserId: user.Id
    }
    ajaxCall("POST", api, JSON.stringify(totalObj), postSqlSuccessCB, postSqlErrorCB);
}
function postSqlSuccessCB(feedback) {
    if (feedback == 1) //just for user
        alert(totalObj.Series.Name + " Season " + totalObj.Episode.SeasonNum + " Episode " + totalObj.Episode.EpisodeName + " inserted ");
    else
        alert("preference already exists");

}
function postSqlErrorCB() {
    alert("ERROR");
}