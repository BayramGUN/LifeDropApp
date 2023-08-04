const loginResponseCookie = getCookie("loginResponseCookie");
let authenticationData = {};
let userFromCookie = {};
console.log(loginResponseCookie);
if(loginResponseCookie.length !== 0) {
  authenticationData = JSON.parse(loginResponseCookie);
  userFromCookie = authenticationData.user; 
}

switch(userFromCookie.role)
{
  case "admin":
    window.location.href = `App/HTML/adminPanel.html`;
    break;
  case "donor":
    window.location.href = `App/HTML/donorPanel.html`;
    break;
  case "hospital":
    window.location.href = `App/HTML/hospitalPanel.html`;
}

async function fetchneedForBloodsJSON() {
  const response = await fetch('https://localhost:7271/needForBloods/biggerThanZero');
  const needForBloods = await response.json();
  return needForBloods;
}
  
fetchneedForBloodsJSON().then((needForBloods) => {
  const bloodNeedsDiv = document.getElementById("needs");
  let image = ``;
  needForBloods.forEach(element => {
        switch(element.bloodType) {
          case "a+":
            image = `<img src="./App/Images/blood_ap.png" class="shadow-md rouded-sm bg-red-400 object-cover w-full p-2" alt="bloodType-pic"></img>`
            break;
          case "b+":
            image = `<img src="./App/Images/blood_bp.png" class="shadow-md rouded-sm bg-red-400 object-cover w-full p-2" alt="bloodType-pic"></img>`
            break;
          case "ab+":
            image = `<img src="./App/Images/blood_abp.png" class="shadow-md rouded-sm bg-red-400 object-cover w-full p-2" alt="bloodType-pic"></img>`
            break;
          case "0+":
            image = `<img src="./App/Images/blood_op.png" class="shadow-md rouded-sm bg-red-400 object-cover w-full p-2" alt="bloodType-pic"></img>`
            break;
            case "0-":
            image = `<img src="./App/Images/blood_on.png" class="shadow-md rouded-sm bg-red-400 object-cover w-full p-2" alt="bloodType-pic"></img>`
            break;
            case "a-":
              image = `<img src="./App/Images/blood_an.png" class="shadow-md rouded-sm bg-red-400 object-cover w-full p-2" alt="bloodType-pic"></img>`
            break;
            case "b-":
              image = `<img src="./App/Images/blood_bn.png" class="shadow-md rouded-sm bg-red-400 object-cover w-full p-2" alt="bloodType-pic"></img>`
              break;
            case "ab-":
                image = `<img src="./App/Images/blood_abn.png" class="shadow-md rouded-sm bg-red-400 object-cover w-full p-2" alt="bloodType-pic"></img>`
              break;
          }
    bloodNeedsDiv.innerHTML += `<div class="bg-gray-200 rounded-md overflow-hidden m-2 shadow-md relative p-1 ">
                               ${image}
                               <div class="m-4">
                               <span class="block text-red-400 text-sm"><b>Quantity of Need:</b> ${element.quantityNeeded} unit</span>
                                <span class="text-red-400 font-bold">${element.hospital.name}</span>
                                <span class="block text-red-400 text-sm">${element.hospital.address}</span>
                               </div>
                             </div>`
          
        })
});

