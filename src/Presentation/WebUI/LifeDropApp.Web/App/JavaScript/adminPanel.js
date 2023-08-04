const loginResponseCookie = getCookie("loginResponseCookie");
const cookieData = JSON.parse(loginResponseCookie);
const userData = cookieData.user;
const token = cookieData.token; 
const role = localStorage.getItem("role");
if(userData.role !== 'admin' && role !== 'admin')
    window.location.href = `../../index.html`;
function registerHospital() {  
    localStorage.setItem("role", 'admin');
    window.location.href = `../../Authentication/HTML/register.html`
}


async function fetchUsersJSON() {
    const response = await fetch(`https://localhost:7271/users`, {
      method: 'GET',
      mode: 'cors',
      headers: {
        'Content-Type': 'application/json',
        'Accept': 'application/json',
        'Authorization': 'Bearer ' + token 
      }
    });
    const users = await response.json();
    return users;
}
let donorsData = {}
fetchUsersJSON().then((users) => {
    fetchHospitalsJSON().then((hospitals) => {
        fetchDonorsJSON().then(donors => {

            const usersDiv = document.getElementById("users");
            users.forEach(user => {
                
                usersDiv.innerHTML += `<div class="bg-gray-200 rounded-md overflow-hidden p-4 m-2 shadow-md relative p-1 ">
                <div class="m-4 justify-between">
                <span class="block text-red-400 text-sm">${user.username}</span>
                </div>
                </div>`;
                
                const hospitalsDiv = document.getElementById("hospitals");
                hospitals.forEach(hospital => {
                    console.log(hospital.id)
                    if(user.id == hospital.userId)
                    hospitalsDiv.innerHTML += `
                    <div class="bg-gray-200 rounded-md overflow-hidden p-4 m-2 shadow-md relative p-1 ">
                    <div class="m-4 justify-between">
                    <span class="block text-red-400 text-sm"><b><i>User:</i></b> ${user.username} <br> => <b><i>Hospital:</i></b> ${hospital.name}</span>
                    <button onClick="blockHospital('${hospital.id}', '${user.id}')">Block</button>
                    </div>
                    </div>`;
                });
                const donorsDiv = document.getElementById("donors");
                donors.forEach(donor => {
                    if(user.id == donor.userId)
                    donorsDiv.innerHTML += `
                    <div class="bg-gray-200 rounded-md overflow-hidden p-4 m-2 shadow-md relative p-1 ">
                    <div class="m-4 justify-between">
                    <span class="block text-red-400 text-sm"><b><i>User:</i></b> ${user.username} <br> => <b><i>Donor:</i></b> ${donor.fullname}  Blood Type: ${donor.bloodType}</span>
                    <button onClick="blockDonor('${donor.id}', '${user.id}')">Block</button>
                    </div>
                    </div>`;
                });
            });
        });
            
    
    });
});

async function fetchHospitalsJSON() {
    const response = await fetch(`https://localhost:7271/hospitals`, {
      method: 'GET',
      mode: 'cors',
      headers: {
        'Content-Type': 'application/json',
        'Accept': 'application/json',
        'Authorization': 'Bearer ' + token 
      }
    });
    const hospitals = await response.json();
    return hospitals;
}
async function fetchDonorsJSON() {
    const response = await fetch(`https://localhost:7271/donors`, {
      method: 'GET',
      mode: 'cors',
      headers: {
        'Content-Type': 'application/json',
        'Accept': 'application/json',
        'Authorization': 'Bearer ' + token 
      }
    });
    const donors = await response.json();
    return donors;
}

function blockDonor(donorId, userId)
{
    const deleteDonorUrl = `https://localhost:7271/donors/delete/${donorId}`; 
    fetch(deleteDonorUrl, {
      method: 'DELETE',
      mode: 'cors',
      headers: {
        'Content-Type': 'application/json',
        'Accept': 'application/json',
        'Authorization': 'Bearer ' + token 
      }
    }).then(response => {
        if(response.status == 200)
        {

            const deleteUserUrl = `https://localhost:7271/users/delete/${userId}`; 
            fetch(deleteUserUrl, {
                method: 'DELETE',
                mode: 'cors',
                headers: {
                    'Content-Type': 'application/json',
                    'Accept': 'application/json',
                    'Authorization': 'Bearer ' + token 
                }
            });
        }
        else {
            alert(`${userId} can not deleted`)
        }
    });
}
function blockHospital(hospitalId, userId)
{
    const deleteAllAnnouncementsUrl = `https://localhost:7271/needForBloods/delete/${hospitalId}`
    fetch(deleteAllAnnouncementsUrl, {
        method: 'DELETE',
        mode: 'cors',
        headers: {
            'Content-Type': 'application/json',
            'Accept': 'application/json',
            'Authorization': 'Bearer ' + token 
        }
    }).then(response => {
        if(response.status == 200) {
            const deleteDonorUrl = `https://localhost:7271/hospitals/delete/${hospitalId}`; 
            fetch(deleteDonorUrl, {
                method: 'DELETE',
                mode: 'cors',
                headers: {
                    'Content-Type': 'application/json',
                    'Accept': 'application/json',
                    'Authorization': 'Bearer ' + token 
                }
            }).then(response => {
                if(response.status == 200)
                { 
                    const deleteUserUrl = `https://localhost:7271/users/delete/${userId}`; 
                    fetch(deleteUserUrl, {
                        method: 'DELETE',
                        mode: 'cors',
                        headers: {
                            'Content-Type': 'application/json',
                            'Accept': 'application/json',
                            'Authorization': 'Bearer ' + token 
                        }
                    });
                }
                else {
                    response.json().then(data => {
                        alert(`${data.message} can not deleted`)
                    });
                }
            });
        }
        else {
            response.json().then(data => {
                alert(`${data.message} can not deleted`)
            });
        }
    });
}