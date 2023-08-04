const loginResponseCookie = getCookie("loginResponseCookie");
const registerResponseCookie = getCookie("registerResponseCookie");
let cookieData = {};
let registerCookieData = {};
if(loginResponseCookie !== "")
  cookieData = JSON.parse(loginResponseCookie);

if(registerResponseCookie !== "") {
  registerCookieData = JSON.parse(registerResponseCookie);
}

console.log(registerResponseCookie)
const userData = cookieData.user;
const token = cookieData.token;
const hospitalUserData = registerCookieData.user;
const hospitalForm = document.getElementById('addHospital');
const adminRole = localStorage.getItem("role");
console.log()

hospitalForm.addEventListener('submit', (e) => {
    e.preventDefault();

    const hospitalName = document.getElementById('hospitalName').value;
    const street = document.getElementById('street').value;
    const country = document.getElementById('country').value;
    const city = document.getElementById('city').value;
    const zipCode = document.getElementById('zip-code').value;
    if(!hospitalName || !street || !country || !city || !zipCode)
        Swal.fire({ 
            icon: 'error',
            title: 'Oops...',
            text: 'You must fill the blanks!',
            confirmButtonColor: '#CC3333',
          });
    else
    {
        Swal.fire({
            title: 'Do you want to add information?',
            showDenyButton: true,
            showCancelButton: true,
            confirmButtonText: 'Save',
            denyButtonText: `Don't save`,
          }).then((result) => {
            if (result.isConfirmed) {
              const addHospitalData = {
                userId: hospitalUserData.id,
                name: hospitalName,
                address: {
                    street: street,
                    city: city,
                    country: country,
                    zipCode: zipCode,
                }
              };
              sendHospitalInfo(addHospitalData);
            }
          });
      }
    });
    
        
function sendHospitalInfo(addHospitalData) {
  var url = 'https://localhost:7271/hospitals/create';
  
  fetch(url, {
    method: 'POST',
    mode: 'cors',
    headers: {
      'Content-Type': 'application/json',
      'Authorization': 'Bearer ' + token
    },
        body: JSON.stringify(addHospitalData)
    }).then(response => {
      if(response.status == 201)
      {
        Swal.fire({ 
          icon: 'success',
          title: 'Your credentials are correct.',
          text: 'Click OK to continue!',
          confirmButtonColor: '#CC3333',
        }).then(() => {
          window.location.href = '../HTML/adminPanel.html';   
        });
      }
      else {

        response.json().then(data => {
          Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: `This user ${data.message}`,
            confirmButtonColor: '#CC3333',
          }).then(result => {
              if(result.isConfirmed) {
                console.log(result);
              }
            });
        });
      }
    })
}
