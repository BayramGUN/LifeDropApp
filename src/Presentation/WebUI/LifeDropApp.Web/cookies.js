function getCookie(cookieName) {
    let name = cookieName + "=";
    let decodedCookie = decodeURIComponent(document.cookie);
    let ca = decodedCookie.split(';');
    for(let i = 0; i <ca.length; i++) {
      let c = ca[i];
      while (c.charAt(0) == ' ') {
        c = c.substring(1);
      }
      if (c.indexOf(name) == 0) {
        return c.substring(name.length, c.length);
      }
    }
    return "";
}


function setCookie(cookieName, cookieValue, expiryDay) {
    const d = new Date();
    d.setTime(d.getTime() + (expiryDay*24*60*60*1000));
    let expires = "expires="+ d.toUTCString();
    document.cookie = cookieName + "=" + JSON.stringify(cookieValue) + ";" + expires + ";path=/";
}
/* function setAdminCookie(cookieName, cookieValue, expiryDay) {
    const d = new Date();
    d.setTime(d.getTime() + (expiryDay*24*60*60*1000));
    let expires = "expires="+ d.toUTCString();
    document.cookie = cookieName + "=" + JSON.stringify(cookieValue) + ";" + expires + ";path=/App/HTML/adminPanel;SameSite=Strict";
}
function setDonorCookie(cookieName, cookieValue, expiryDay) {
    const d = new Date();
    d.setTime(d.getTime() + (expiryDay*24*60*60*1000));
    let expires = "expires="+ d.toUTCString();
    document.cookie = cookieName + "=" + JSON.stringify(cookieValue) + ";" + expires + ";path=/App/HTML/donorPanel;SameSite=Strict";
}
function setHospitalCookie(cookieName, cookieValue, expiryDay) {
    const d = new Date();
    d.setTime(d.getTime() + (expiryDay*24*60*60*1000));
    let expires = "expires="+ d.toUTCString();
    document.cookie = cookieName + "=" + JSON.stringify(cookieValue) + ";" + expires + ";path=/App/HTML/hospitalPanel;SameSite=Strict";
} */

function deleteCookie(cookieName) {
    document.cookie = `${cookieName}=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;`;
}

function deleteAllCookies() {
    const cookies = document.cookie.split(";");
  
    for (let i = 0; i < cookies.length; i++) {
      const cookie = cookies[i];
      const eqPos = cookie.indexOf("=");
      const cookieName = eqPos > -1 ? cookie.substr(0, eqPos) : cookie;
      document.cookie = `${cookieName}=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;`;
    }
}

function logout() {
  deleteCookie("responseCookie");
  deleteAllCookies();
  localStorage.clear();
  window.location.href = `../../index.html`;
}