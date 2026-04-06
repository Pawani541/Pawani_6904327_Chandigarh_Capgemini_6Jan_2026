const API_URL = "/api";
let editingId = null;

async function login() {
  const username = document.getElementById("username").value.trim();
  const password = document.getElementById("password").value.trim();
  const errorMsg = document.getElementById("error-msg");
  if (!username || !password) { errorMsg.textContent = "Please fill in all fields."; return; }
  try {
    const response = await fetch(API_URL + "/auth/login", {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({ username, password })
    });
    if (!response.ok) { errorMsg.textContent = "Invalid username or password."; return; }
    const data = await response.json();
    localStorage.setItem("token", data.token);
    window.location.href = "/dashboard.html";
  } catch (err) { errorMsg.textContent = "Could not connect to server."; }
}

function renderTable(transactions) {
  const tbody = document.getElementById("table-body");
  tbody.innerHTML = "";
  if (transactions.length === 0) {
    tbody.innerHTML = "<tr><td colspan='5' style='text-align:center'>No transactions found</td></tr>";
    return;
  }
  transactions.forEach(t => {
    const row = document.createElement("tr");
    row.innerHTML =
      "<td>" + t.amount + "</td>" +
      "<td>" + new Date(t.date).toLocaleDateString() + "</td>" +
      "<td>" + t.type + "</td>" +
      "<td><button class='edit-btn' onclick='openEdit(" + t.id + "," + t.amount + ",\"" + t.type + "\")'>Edit</button></td>" +
      "<td><button class='delete-btn' onclick='deleteTransaction(" + t.id + ")'>Delete</button></td>";
    tbody.appendChild(row);
  });
}

async function loadTransactions() {
  const token = localStorage.getItem("token");
  if (!token) { window.location.href = "/index.html"; return; }
  try {
    const response = await fetch(API_URL + "/transactions", {
      headers: { "Authorization": "Bearer " + token }
    });
    if (!response.ok) { console.log("Failed:", response.status); return; }
    const transactions = await response.json();
    renderTable(transactions);
  } catch (err) { console.log("Error:", err); }
}

async function addTransaction() {
  const token = localStorage.getItem("token");
  const amountInput = document.getElementById("new-amount");
  const typeInput = document.getElementById("new-type");
  const msg = document.getElementById("form-msg");
  const amount = amountInput.value;
  const type = typeInput.value;
  if (!amount || amount <= 0) {
    msg.style.color = "red";
    msg.textContent = "Please enter a valid amount.";
    return;
  }
  try {
    const response = await fetch(API_URL + "/transactions", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        "Authorization": "Bearer " + token
      },
      body: JSON.stringify({ amount: parseFloat(amount), type: type, date: new Date().toISOString() })
    });
    if (response.ok) {
      const updatedList = await response.json();
      renderTable(updatedList);
      amountInput.value = "";
      msg.style.color = "green";
      msg.textContent = "Transaction added successfully!";
      setTimeout(() => { msg.textContent = ""; }, 3000);
    } else {
      msg.style.color = "red";
      msg.textContent = "Failed to add. Error: " + response.status;
    }
  } catch (err) {
    msg.style.color = "red";
    msg.textContent = "Could not connect to server.";
  }
}

function openEdit(id, amount, type) {
  editingId = id;
  document.getElementById("edit-amount").value = amount;
  document.getElementById("edit-type").value = type;
  document.getElementById("edit-modal").style.display = "flex";
}

function closeModal() {
  document.getElementById("edit-modal").style.display = "none";
  editingId = null;
}

async function saveEdit() {
  const token = localStorage.getItem("token");
  const amount = document.getElementById("edit-amount").value;
  const type = document.getElementById("edit-type").value;
  const response = await fetch(API_URL + "/transactions/" + editingId, {
    method: "PUT",
    headers: {
      "Content-Type": "application/json",
      "Authorization": "Bearer " + token
    },
    body: JSON.stringify({ amount: parseFloat(amount), type: type, date: new Date().toISOString() })
  });
  if (response.ok) {
    const updatedList = await response.json();
    renderTable(updatedList);
    closeModal();
  }
}

async function deleteTransaction(id) {
  const token = localStorage.getItem("token");
  if (!confirm("Are you sure you want to delete this transaction?")) return;
  const response = await fetch(API_URL + "/transactions/" + id, {
    method: "DELETE",
    headers: { "Authorization": "Bearer " + token }
  });
  if (response.ok) {
    const updatedList = await response.json();
    renderTable(updatedList);
  }
}

function logout() {
  localStorage.removeItem("token");
  window.location.href = "/index.html";
}

if (document.getElementById("table-body")) { loadTransactions(); }
