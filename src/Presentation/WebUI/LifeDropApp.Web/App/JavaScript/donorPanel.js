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
async function fetchDonorInfoJSON(userData) {
  const response = await fetch(`https://localhost:7271/donors/get/users/${userData.id}`, {
    method: 'GET',
    mode: 'cors',
    headers: {
      'Content-Type': 'application/json',
      'Accept': 'application/json',
      'Authorization': 'Bearer ' + cookieData.token
    }
  });
  const donorInfo = await response.json();
  return donorInfo;
}

async function fetchNeedForBloodsJSON(donorInfo) {
  const response = await fetch(`https://localhost:7271/needForBloods/${donorInfo.bloodType}`, {
    method: 'GET',
    headers: {
      'Content-Type': 'application/json'
    }
  });
  const needForBloods = await response.json();
  return needForBloods;
}

fetchDonorInfoJSON(userData).then((data) => {
  const donorInfo = data;
  console.log(donorInfo.fullname)
  document.getElementById('welcomeDonor').innerText = `Welcome ${donorInfo.fullname}` 
  document.getElementById('donorBloodType').innerText = `Your BloodType is ${donorInfo.bloodType}` 
  fetchNeedForBloodsJSON(donorInfo).then((needForBloods) => {
    const bloodNeedsDiv = document.getElementById("needs");
    let image = ``;
    needForBloods.forEach(element => {  
      switch(donorInfo.bloodType.toLowerCase()) {
        case "a+":
          image = `<img src="../Images/blood_ap.png" class="shadow-md rouded-sm bg-red-400 object-cover w-full p-2" alt="bloodType-pic"></img>`
          break;
        case "b+":
          image = `<img src="../Images/blood_bp.png" class="shadow-md rouded-sm bg-red-400 object-cover w-full p-2" alt="bloodType-pic"></img>`
          break;
        case "ab+":
          image = `<img src="../Images/blood_abp.png" class="shadow-md rouded-sm bg-red-400 object-cover w-full p-2" alt="bloodType-pic"></img>`
          break;
        case "0+":
          image = `<img src="../Images/blood_op.png" class="shadow-md rouded-sm bg-red-400 object-cover w-full p-2" alt="bloodType-pic"></img>`
          break;
        case "0-":
          image = `<img src="../Images/blood_on.png" class="shadow-md rouded-sm bg-red-400 object-cover w-full p-2" alt="bloodType-pic"></img>`
          break;
        case "a-":
          image = `<img src="../Images/blood_an.png" class="shadow-md rouded-sm bg-red-400 object-cover w-full p-2" alt="bloodType-pic"></img>`
          break;
        case "b-":
          image = `<img src="../Images/blood_bn.png" class="shadow-md rouded-sm bg-red-400 object-cover w-full p-2" alt="bloodType-pic"></img>`
          break;
        case "ab-":
          image = `<img src="../Images/blood_abn.png" class="shadow-md rouded-sm bg-red-400 object-cover w-full p-2" alt="bloodType-pic"></img>`
          break;
      }

      if(element.bloodType === donorInfo.bloodType.toLowerCase())
        bloodNeedsDiv.innerHTML += `
                  <div class="bg-gray-200 rounded-md overflow-hidden p-4 m-2 shadow-md relative p-1 ">
                      ${image}
                      <div class="m-4 justify-between">
                        <span class="text-red-400 font-bold">${element.hospital.name}</span>
                        <span class="block text-red-400 text-sm">${element.hospital.address}</span>
                        <span class="block text-red-400 text-sm"><b>Quantity of Need:</b> ${element.quantityNeeded} unit</span>
                      </div>
                  </div>`
                  
        });
      }); 
    });
              
              