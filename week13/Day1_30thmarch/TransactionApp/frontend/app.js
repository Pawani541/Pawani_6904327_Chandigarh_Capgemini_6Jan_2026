const API_URL = "http://localhost:5032/api";
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
    window.location.href = "dashboard.html";
  } catch (err) { errorMsg.textContent = "Could not connect to server."; }
}

async function loadTransactions() {
  const token = localStorage.getItem("token");
  if (!token) { window.location.href = "index.html"; return; }
  const response = await fetch(API_URL + "/transactions", {
    headers: { "Authorization": "Bearer " + token }
  });
  const transactions = await response.json();
  const tbody = document.getElementById("table-body");
  tbody.innerHTML = "";
  transactions.forEach(t => {
    tbody.innerHTML += "<tr>" +
      "<td>" + t.amount + "</td>" +
      "<td>" + new Date(t.date).toLocaleDateString() + "</td>" +
      "<td>" + t.type + "</td>" +
      "<td><button class='edit-btn' onclick='openEdit(" + t.id + "," + t.amount + ",\"" + t.type + "\")'>Edit</button></td>" +
      "<td><button class='delete-btn' onclick='deleteTransaction(" + t.id + ")'>Delete</button></td>" +
      "</tr>";
  });
}

async function addTransaction() {
  const token = localStorage.getItem("token");
  const amount = document.getElementById("new-amount").value;
  const type = document.getElementById("new-type").value;
  const msg = document.getElementById("form-msg");
  if (!amount) { msg.textContent = "Please enter an amount."; return; }
  await fetch(API_URL + "/transactions", {
    method: "POST",
    headers: { "Content-Type": "application/json", "Authorization": "Bearer " + token },
    body: JSON.stringify({ amount: parseFloat(amount), type: type, date: new Date() })
  });
  document.getElementById("new-amount").value = "";
  msg.textContent = "";
  loadTransactions();
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
  await fetch(API_URL + "/transactions/" + editingId, {
    method: "PUT",
    headers: { "Content-Type": "application/json", "Authorization": "Bearer " + token },
    body: JSON.stringify({ amount: parseFloat(amount), type: type, date: new Date() })
  });
  closeModal();
  loadTransactions();
}

async function deleteTransaction(id) {
  const token = localStorage.getItem("token");
  if (!confirm("Are you sure you want to delete this transaction?")) return;
  await fetch(API_URL + "/transactions/" + id, {
    method: "DELETE",
    headers: { "Authorization": "Bearer " + token }
  });
  loadTransactions();
}

function logout() {
  localStorage.removeItem("token");
  window.location.href = "index.html";
}

if (document.getElementById("table-body")) { loadTransactions(); }
