const responseCookie = getCookie("loginResponseCookie");
const role = localStorage.getItem("role");

let registerRole = "donor";
if(role !== null && role.toLocaleLowerCase() == "admin"){
  registerRole = "hospital";
  document.getElementById("head").innerHTML += `<button class="right-12" onClick="logout()">Log Out</button>`
  if(responseCookie != ""){
    authenticationData = JSON.parse(responseCookie);
    userData = authenticationData.user;
  }
}
console.log(registerRole)
const registerForm = document.getElementById('register');
registerForm.addEventListener('submit', (e) => {
    e.preventDefault();

    const username = document.getElementById('username').value;
    const password = document.getElementById('password').value;
    const phone = document.getElementById('phone').value;
    const email = document.getElementById('email').value;
    if(!username || !password || !phone || !email)
        Swal.fire({ 
            icon: 'error',
            title: 'Oops...',
            text: 'You must fill the blanks!',
            confirmButtonColor: '#CC3333',
          });
    else
    {
        const registerData = {
            username: username,
            password: password,
            email: email,
            phone: phone,
            role: registerRole
        }
        sendRegtister(registerData);
    }
});

function sendRegtister(registerData) {
    var url = 'https://localhost:7271/auth/register';
    fetch(url, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json'
        },
        body: JSON.stringify(registerData)
    }).then(response => {
      response.json().then(data => {    
        if(response.status == 201)
        {
          setCookie("registerResponseCookie", data, 1)
          /* if(registerRole == 'hospital')
            setHospitalCookie("hospitalCookie", data, 1);
          if(registerRole == 'donor')
            setDonorCookie("donorCookie", data, 1) */
          Swal.fire({ 
            icon: 'success',
            title: 'Your registration is valid.',
            text: 'Click OK to fill informations!',
            confirmButtonColor: '#CC3333',
          }).then(() => {
            switch(registerRole) {
              case "hospital": {
                  
                  window.location.href = '../../../App/HTML/addHospitalInfo.html';}
                break;
              case "donor": {
                  window.location.href = '../../../App/HTML/addDonorInfo.html';
                }
                break;
            }
            });
          }
          else { 
          Swal.fire(
            {
              icon: 'error',
              title: 'ERROR',
              text: data.message
            }
            );}
          });
          
      });
}
