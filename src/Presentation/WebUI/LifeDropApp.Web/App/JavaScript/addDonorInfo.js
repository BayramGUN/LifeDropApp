const loginResponseCookie = getCookie("loginResponseCookie");
const registerResponseCookie = getCookie("registerResponseCookie");
let cookieData = {};
if(loginResponseCookie !== "") {
  cookieData = JSON.parse(loginResponseCookie);
}
else if(registerResponseCookie !== "") {
  cookieData = JSON.parse(registerResponseCookie);
}
const userData = cookieData.user;


const donorForm = document.getElementById('addDonor');
if(userData.donorName[0] !== undefined)
  window.location.href = `../HTML/donorPanel.html`;

donorForm.addEventListener('submit', (e) => {
  e.preventDefault();

  const firstname = document.getElementById('firstname').value;
  const lastname = document.getElementById('lastname').value;
  const bloodType = document.getElementById('blood-type').value;
  const age = document.getElementById('age').value;
  const street = document.getElementById('street').value;
  const country = document.getElementById('country').value;
  const city = document.getElementById('city').value;
  const zipCode = document.getElementById('zip-code').value;
  const userId = userData.id;

  if(!firstname || !lastname || !bloodType || !street || !city || !country || !zipCode)
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
        /* Read more about isConfirmed, isDenied below */
        if (result.isConfirmed) {
            const addDonorData = {
                userId: userId,
                firstname: firstname,
                lastname: lastname,
                bloodType: bloodType.toLowerCase(),
                age: age,
                address: {
                    street: street,
                    city: city,
                    country: country,
                    zipCode: zipCode,
                }
            } 
            //console.log(JSON.stringify(addDonorData));
          
            sendDonorInfo(addDonorData);
          }
        });
  }  
});

function sendDonorInfo(addDonorData) {
    var url = 'https://localhost:7271/donors/create';
    fetch(url, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
          'Authorization': 'Bearer ' + cookieData.token 
        },
        body: JSON.stringify(addDonorData)
    }).then(response => { 
      response.json().then(data => {
      if(response.status == 201)
      {
        localStorage.setItem("donorName", data.firstname)
        Swal.fire({ 
          icon: 'success',
          title: 'Your credentials are correct.',
          text: 'Click OK to fill informations!',
          confirmButtonColor: '#CC3333',
        }).then(() => {
          window.location.href = '../HTML/donorPanel.html';
        });
      }
      else
          
          Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: `${userData.username} ${data.message}` ,
            confirmButtonColor: '#CC3333',
          });
        });
      }); 

}