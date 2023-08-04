const loginForm = document.getElementById('login');

loginForm.addEventListener('submit', (e) => {
    e.preventDefault();

    const username = document.getElementById('username').value;
    const password = document.getElementById('password').value;
    if(!username || !password)
        Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: 'You must fill the blanks!',
            confirmButtonColor: '#CC3333',
          });                
    else
    {
        const loginData = {
            username: username,
            password: password
        }
        sendLogin(loginData);
    }
});

function sendLogin(loginData) {
    var url = 'https://localhost:7271/auth/login';
    fetch(url, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json'
        },
        body: JSON.stringify(loginData)
    }).then(response => {
      if(response.status == 200)
      {
          response.json().then(data => { 
            setCookie("loginResponseCookie", data, 1);
         /*  if(data.user.role == 'admin')
            setAdminCookie("adminCookie", data, 1);
          else if(data.user.role == 'donor')
            setDonorCookie("donorCookie", data, 1);
          else if(data.user.role == 'hospital')
            setHospitalCookie("hospitalCookie", data, 1) */
          Swal.fire({ 
            icon: 'success',
            title: 'Your credentials are correct.',
            text: `Click 'OK' to go to the main page!`,
            confirmButtonColor: '#CC3333',
          }).then((result) => {
            if(result.isConfirmed) {
              
              console.log(data.user.role)
              switch(data.user.role) {
                case "admin":
                  window.location.href = `../../App/HTML/adminPanel.html`;
                  break;
                case "donor":
                  window.location.href = `../../App/HTML/donorPanel.html`;
                  break;
                case "hospital":
                  window.location.href = `../../App/HTML/hospitalPanel.html`;
                  break;         
                default:
                  window.location.href = `../../index.html`;
                  break;
              }
            }
            });
          });
        }
        else {
          response.json().then(data => {
            Swal.fire({ 
              icon: 'error',
              title: data.message,
              text: `Click 'OK' to try again!`,
              confirmButtonColor: '#CC3333',
            });
          });
        }
      });
}
