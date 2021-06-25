function navBarVisability() {
    /*$('#navBar').html= createNavBar();*/
    if (localStorage.user != null) {
        user = JSON.parse(localStorage["user"]);
        $("#userName").text("  Hello " + user.FirstName + " " + user.LastName);
        document.getElementById("signUp").style.visibility = "hidden";
        document.getElementById("login").style.visibility = "hidden";
        document.getElementById("editUser").style.visibility = "visibility";
        document.getElementById("exit").style.visibility = "visibility";
        if (localStorage.profileSrc != undefined)
            userProfile = JSON.parse(localStorage.profileSrc);
        else
            userProfile = "https://image.ibb.co/jw55Ex/def_face.jpg";
        $('#profile').html(`<img id="profileImg" style="height:50px; width:40px; border-radius: 25px; margin-right: 8px;" src="` + userProfile + `"/>`);

        if (user.FirstName == "Administrator") {
            document.getElementById("admin").style.visibility = "visibility";
        }
        else {
            document.getElementById("admin").style.visibility = "hidden";
        }
    }
    else {
        document.getElementById("signUp").style.visibility = "visibility";
        document.getElementById("login").style.visibility = "visibility";
        document.getElementById("editUser").style.visibility = "hidden";
        document.getElementById("exit").style.visibility = "hidden";
        document.getElementById("admin").style.visibility = "hidden";
        document.getElementById("view").style.visibility = "hidden";
    }
}

function exitFunc() {
    localStorage.clear();
    document.location = 'insert_signup.html';
    document.getElementById("pForm").reset();
}

function createNavBar() {
    return `<nav class="navbar navbar-inverse ">
        <div class="container-fluid">
            <div class="navbar-header">
                <!--<a class="navbar-brand" href="#">The Movie H & H</a>-->
            </div>
            <ul class="nav navbar-nav justify-content-between">
                <li><div id="userName"></div></li>
                <li><a class="toblack" href="homePage.html"> Home</a></li>
                <li><a class="toblack" href="insert_signup.html">Search</a></li>
                <li><a class="toblack" id="view" href="view.html">View</a></li>
                <li><a class="toblack" id="admin" href="admin.html">Admin</a></li>

            </ul>

            <ul class="nav navbar-nav navbar-right">
                <li><a onclick="signUpClick()" id="signUp" class="toblack"><span class="glyphicon glyphicon-user singupc"></span> Sign Up</a></li>
                <li><a id="login" href="login.html" class="toblack"><span class="glyphicon glyphicon-log-in "></span> Login</a></li>
                <li><a onclick="exitFunc()" id="exit" class="toblack"><span class="glyphicon glyphicon-log-in "></span> Exit</a></li>
            </ul>
        </div>
    </nav>`
}