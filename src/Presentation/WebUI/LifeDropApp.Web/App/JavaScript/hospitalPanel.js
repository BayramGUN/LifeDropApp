const loginResponseCookie = getCookie("loginResponseCookie");
let cookieData = {};
if(loginResponseCookie !== "") {
  cookieData = JSON.parse(loginResponseCookie);
}

const userData = cookieData.user;


async function fetchHospitalInfoJSON(userData) {
  const response = await fetch(`https://localhost:7271/hospitals/get/users/${userData.id}`, {
    method: 'GET',
    mode: 'cors',
    headers: {
      'Content-Type': 'application/json',
      'Accept': 'application/json',
      'Authorization': 'Bearer ' + cookieData.token
    }
  });
  const hospitalInfo = await response.json();
  console.log(hospitalInfo.id)
  return hospitalInfo;
}
document.getElementById("welcomeHospital").innerText = userData.hospitalName[0];
document.getElementById("hospitalNameOnForm").innerText = userData.hospitalName[0];


const addNeedForBLoodForm = document.getElementById('addNeedForBlood');

fetchHospitalInfoJSON(userData).then(data =>{
  //console.log(data.id);
  addNeedForBLoodForm.addEventListener('submit', (e) => {
    e.preventDefault();
    const quantityNeeded = document.getElementById('quantityNeeded').value;
    const bloodType = document.getElementById('bloodType').value;
    
    if(!quantityNeeded || !bloodType)
    Swal.fire({ 
      icon: 'error',
      title: 'Oops...',
      text: 'You must fill the blanks!',
      confirmButtonColor: '#CC3333',
    });
    else {
      Swal.fire({
        title: 'Do you want to add announcement of need for blood?',
        showDenyButton: true,
        showCancelButton: true,
        confirmButtonText: 'Yes',
        denyButtonText: `No`,
      }).then(result => {
        if(result.isConfirmed) {
          const needForBloodData = {
            quantityNeeded: quantityNeeded,
            bloodType: bloodType.toLowerCase(),
            hospitalId: data.id
          }
          sendBloodNeedInfo(needForBloodData);
        }
      });
    }
  });
}); 


function sendBloodNeedInfo(needForBloodData) {
  console.log(needForBloodData);
  var url = 'https://localhost:7271/needForBloods/create';
  fetch(url, {
    method: 'POST',
    mode: 'cors',
    headers: {
      'Content-Type': 'application/json',
      'Accept': 'application/json',
      'Authorization': 'Bearer ' + cookieData.token 
    },
    body: JSON.stringify(needForBloodData)
  }).then(response => { 
    response.json().then(data => {
      console.log(response);
    if(response.status == 201)
    {
        Swal.fire({ 
          icon: 'success',
          title: `Blood type ${data.request.bloodType} is added`,
          text: data.message,
          confirmButtonColor: '#CC3333',
        }).then(result => {
          if(result.isConfirmed)
            location.reload();
        });
      }
      else {
        Swal.fire({
          icon: 'error',
          title: 'Oops...',
          text: `${userData.username} ${data.message}` ,
          confirmButtonColor: '#CC3333',
        });
      }
    });
  });
}

async function fetchNeedForBloodsJSON(hospitalInfo) {
  const response = await fetch(`https://localhost:7271/needForBloods/byHospital?hospitalId=${hospitalInfo.id}`, {
    method: 'GET',
    headers: {
      'Content-Type': 'application/json',
      'Accept': 'application/json'
    }
  });
  const needForBloods = await response.json();
  return needForBloods;
}

fetchHospitalInfoJSON(userData).then(data =>{
  fetchNeedForBloodsJSON(data).then((needForBloods) => {
    const bloodNeedsDiv = document.getElementById("bloodNeedsDiv");
    let image = ``;
    needForBloods.forEach(element => {  
      switch(element.bloodType.toLowerCase()) {
        case "a+":
          image = `<img src="../Images/blood_ap.png" class="shadow-md rouded-md bg-red-400 object-cover w-full p-2" alt="bloodType-pic"></img>`
          break;
        case "b+":
          image = `<img src="../Images/blood_bp.png" class="shadow-md rouded-md bg-red-400 object-cover w-full p-2" alt="bloodType-pic"></img>`
          break;
        case "ab+":
          image = `<img src="../Images/blood_abp.png" class="shadow-md rouded-md bg-red-400 object-cover w-full p-2" alt="bloodType-pic"></img>`
          break;
        case "0+":
          image = `<img src="../Images/blood_op.png" class="shadow-md rouded-md bg-red-400 object-cover w-full p-2" alt="bloodType-pic"></img>`
          break;
        case "0-":
          image = `<img src="../Images/blood_on.png" class="shadow-md rouded-md bg-red-400 object-cover w-full p-2" alt="bloodType-pic"></img>`
          break;
        case "a-":
          image = `<img src="../Images/blood_an.png" class="shadow-md rouded-md bg-red-400 object-cover w-full p-2" alt="bloodType-pic"></img>`
          break;
        case "b-":
          image = `<img src="../Images/blood_bn.png" class="shadow-md rouded-md bg-red-400 object-cover w-full p-2" alt="bloodType-pic"></img>`
          break;
        case "ab-":
          image = `<img src="../Images/blood_abn.png" class="shadow-md rouded-md bg-red-400 object-cover w-full p-2" alt="bloodType-pic"></img>`
          break;
        }
        bloodNeedsDiv.innerHTML += `
          <div class="bg-gray-200 rounded-md overflow-hidden p-4 m-2 shadow-md relative p-1 ">
            <button id="donate" onClick="deleteNeedForBlood('${element.id.toString()}')" class="bg-red-500 hover:bg-red-600 text-sm text-white w-full  rounded-full mb-2 focus:outline-none p-2">Remove Announcement</button>
            ${image}
            <div class="m-4 justify-between">
              <span class="block text-red-400 text-sm"><b>Quantity of Need:</b> ${element.quantityNeeded} unit</span>
              <div class="md:flex md:flex-row w-full">    
                <label class="md:visible text-sm text-red-500 md:m-2 md:text-justify sm:invisible" for="quantityNeeded">Quantity (Unit):</label> 
                <input type="number" min="0" id='${element.id.toString()}' name="quantityNeeded" class="text-sm w-full p-1 focus:outline-none border-b-2 border-gray-600 focus:border-red-500 bg-transparent md:ml-4">
              </div>
              <button id="donate" onClick="getDonate('${element.id.toString()}')" class="bg-red-500 hover:bg-red-600 text-sm text-white w-full mt-8 rounded-md focus:outline-none p-2">Get Donate</button>
            </div>
          </div>`
                  
        });
      }); 
    });            
              

function getDonate(bloodNeedId) {
  const quantityOfDonate = document.getElementById(`${bloodNeedId}`).value;
  var url = `https://localhost:7271/needForBloods/quantities/${bloodNeedId}`;
  
  fetch(url, {
    method: 'PATCH',
    mode: 'cors',
    headers: {
      'Content-Type': 'application/json',
      'Authorization': 'Bearer ' + cookieData.token 
    },
    body: JSON.stringify(quantityOfDonate)
    }).then(response => {
      console.log(response.status)
      if(response.status == 202)
      {
        
        Swal.fire({
          icon: 'success',
          title: 'Operation Succes!',
          text: 'Blood donation successfully received',
          confirmButtonColor: '#CC3333',
        }).then(result => {
          console.log(result)
          if(result.isConfirmed)
            location.reload();
        });
      }
      else {
        response.json().then(data => {
          Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: `${data.message}`,
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

function deleteNeedForBlood(bloodNeedId) {
  Swal.fire({
    title: 'Do you want to add announcement of need for blood?',
    showDenyButton: true,
    showCancelButton: true,
    confirmButtonText: 'Yes',
    denyButtonText: `No`,
  }).then(result => {
    if(result.isConfirmed) {
      
      var url = `https://localhost:7271/needForBloods/delete?id=${bloodNeedId}`;
      
      fetch(url, {
        method: 'DELETE',
        mode: 'cors',
        headers: {
          'Content-Type': 'application/json',
          'Authorization': 'Bearer ' + cookieData.token 
        },
      }).then(response => {
        response.json().then(data => {
          Swal.fire({
            icon: 'success',
            title: 'Operation Succes!',
            text: data.message,
            confirmButtonColor: '#CC3333',
          }).then(result => {
            console.log(result)
            if(result.isConfirmed)
              location.reload();
          });
        })
      });
    }
  });
    
}

    // Function to fetch and populate the dropdown
    async function populateBloodTypes() {
        try {
            const response = await fetch('../localData/bloodTypes.json'); // Replace 'path/to/bloodTypes.json' with the actual path to your .json file.
            const bloodTypes = await response.json();
            const dropdown = document.getElementById('bloodType');
            
            // Clear existing options
            dropdown.innerHTML = '';
            
            // Populate the dropdown with options from the .json file
            bloodTypes.forEach(bloodType => {
                const option = document.createElement('option');
                option.text = bloodType;
                dropdown.add(option);
            });
        } catch (error) {
            console.error('Error loading blood types:', error);
        }
    }

    // Call the function to populate the dropdown when the page loads
    populateBloodTypes();
