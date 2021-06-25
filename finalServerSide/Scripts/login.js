$(document).ready(function () {
    $("#pForm").submit(submit);
    //navBarVisability();
});

function submit() {
    confirmUser();
    return false;
}
function confirmUser() {
    let email = $('#emailTB').val();
    let pass = $('#passwordTB').val();
    let api = "../api/Users?email=" + email + "&password=" + pass;
    ajaxCall("GET", api, "", getLoginSuccessCB, getLoginErrorCB);
}

function getLoginSuccessCB(user) {
    storeToLocalStorage(user);
    let tmp = JSON.parse(localStorage["user"]);
    userName = tmp.FirstName + " " + tmp.LastName;
    swal("Welcome!", userName, "success");
    if (user.FirstName == "Administrator") {
        setTimeout(function () { location.href = 'admin.html'; }, 3000);
    }
    else
        setTimeout(function () { location.href = 'homePage.html'; }, 3000);
}

function storeToLocalStorage(user) {
    let userToStore = {
        Id: user.Id,
        FirstName: user.FirstName,
        LastName: user.LastName,
        Email: user.Email
    }
    localStorage.setItem("user", JSON.stringify(userToStore));
    storeProfile(userToStore.Email);
}

function getLoginErrorCB(err) {
    if (err.status == "404")
        alert("got 404: " + err.responseJSON.Message);
    else
        alert(err.status);
}

function storeProfile(email) {
    const ref = firebase.storage().ref();
    try {
        ref.child(email).getDownloadURL()
            .then(url => {
                console.log(url);
                alert("image here!");
                localStorage.setItem("profileSrc", JSON.stringify(url));

            })
    }
    catch (error) {
        console.error(error);
        let avatarImg = "https://image.ibb.co/jw55Ex/def_face.jpg";
        localStorage.setItem("profileSrc", JSON.stringify(avatarImg));
    }

}
