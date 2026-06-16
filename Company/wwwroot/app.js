const empApi = "https://localhost:7163/api/Employees";
const depApi = "https://localhost:7163/api/Departments";

let employees = [];
let currentPage = 1;
const pageSize = 5;

let sortState = {
    name: true,
    department: true
};


// ================= INIT =================
loadDepartments();
loadEmployees();


// ================= DATE FORMAT FIX =================
function formatDate(dateStr) {
    if (!dateStr) return "";

    const date = new Date(dateStr);

    return date.toLocaleDateString("en-US", {
        year: "numeric",
        month: "short",
        day: "2-digit"
    });
}


// ================= LOAD DEPARTMENTS =================
async function loadDepartments() {

    const res = await fetch(depApi);
    const data = await res.json();

    const select = document.getElementById("departmentSelect");
    select.innerHTML = '<option value="">Select Department</option>';

    data.forEach(dep => {
        select.innerHTML += `
        <option value="${dep.departmentId}">
            ${dep.departmentName}
        </option>`;
    });
}


// ================= LOAD EMPLOYEES =================
async function loadEmployees() {
    const res = await fetch(empApi);
    employees = await res.json();
    renderTable();
}


// ================= RENDER TABLE =================
function renderTable() {

    const start = (currentPage - 1) * pageSize;
    const end = start + pageSize;

    const paged = employees.slice(start, end);

    let rows = "";

    paged.forEach(emp => {

        rows += `
        <tr>
            <td data-label="Name">${emp.fullName}</td>
            <td data-label="Email">${emp.email}</td>
            <td data-label="Mobile">${emp.mobileNumber}</td>
            <td data-label="Department">${emp.departmentName ?? ""}</td>
            <td data-label="Job">${emp.jobTitle}</td>
            <td data-label="Hire Date">${formatDate(emp.hireDate)}</td>
            <td data-label="Actions">

                <button class="btn btn-warning btn-sm mb-2"
                    onclick='editEmployee(${JSON.stringify(emp)})'>
                    Edit
                </button>

                <button class="btn btn-danger btn-sm mb-2"
                    onclick='deleteEmployee(${emp.employeeId})'>
                    Delete
                </button>

            </td>
        </tr>`;
    });

    document.getElementById("empTable").innerHTML = rows;

    renderPagination();
}


// ================= PAGINATION =================
function renderPagination() {

    const pages = Math.ceil(employees.length / pageSize);

    let html = "";

    for (let i = 1; i <= pages; i++) {
        html += `
        <button class="btn btn-sm ${i === currentPage ? 'btn-dark' : 'btn-outline-dark'} mx-1"
            onclick="goToPage(${i})">
            ${i}
        </button>`;
    }

    document.getElementById("pagination").innerHTML = html;
}

function goToPage(page) {
    currentPage = page;
    renderTable();
}


// ================= SORT =================
function sortByName() {

    employees.sort((a, b) =>
        sortState.name
            ? a.fullName.localeCompare(b.fullName)
            : b.fullName.localeCompare(a.fullName)
    );

    sortState.name = !sortState.name;
    renderTable();
}

function sortByDepartment() {

    employees.sort((a, b) =>
        sortState.department
            ? (a.departmentName ?? "").localeCompare(b.departmentName ?? "")
            : (b.departmentName ?? "").localeCompare(a.departmentName ?? "")
    );

    sortState.department = !sortState.department;
    renderTable();
}


// ================= MODAL =================
function openAddModal() {
    clearForm();
    new bootstrap.Modal(document.getElementById("empModal")).show();
}


// ================= SAVE =================
async function saveEmployee() {

    const id = document.getElementById("empId").value;

    const employee = {
        fullName: document.getElementById("fullName").value,
        email: document.getElementById("email").value,
        mobileNumber: document.getElementById("mobile").value,
        departmentId: parseInt(document.getElementById("departmentSelect").value),
        jobTitle: document.getElementById("jobTitle").value,
        hireDate: document.getElementById("hireDate").value,
        isActive: document.getElementById("isActive").value === "true"
    };

    const url = id ? `${empApi}/${id}` : empApi;
    const method = id ? "PUT" : "POST";

    await fetch(url, {
        method,
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(employee)
    });

    bootstrap.Modal.getInstance(document.getElementById("empModal")).hide();

    Swal.fire("Success", "Saved successfully", "success");

    loadEmployees();
}


// ================= EDIT =================
function editEmployee(emp) {

    document.getElementById("empId").value = emp.employeeId;
    document.getElementById("fullName").value = emp.fullName;
    document.getElementById("email").value = emp.email;
    document.getElementById("mobile").value = emp.mobileNumber;
    document.getElementById("departmentSelect").value = emp.departmentId;
    document.getElementById("jobTitle").value = emp.jobTitle;
    document.getElementById("hireDate").value = emp.hireDate.split("T")[0];
    document.getElementById("isActive").value = emp.isActive;

    new bootstrap.Modal(document.getElementById("empModal")).show();
}


// ================= DELETE =================
async function deleteEmployee(id) {

    const result = await Swal.fire({
        title: "Are you sure?",
        text: "This employee will be deleted permanently",
        icon: "warning",
        showCancelButton: true,
        confirmButtonText: "Yes, delete"
    });

    if (!result.isConfirmed) return;

    await fetch(`${empApi}/${id}`, { method: "DELETE" });

    Swal.fire("Deleted", "Employee removed successfully", "success");

    loadEmployees();
}


// ================= CLEAR =================
function clearForm() {
    document.getElementById("empId").value = "";
    document.getElementById("fullName").value = "";
    document.getElementById("email").value = "";
    document.getElementById("mobile").value = "";
    document.getElementById("jobTitle").value = "";
    document.getElementById("hireDate").value = "";
}


// ================= SEARCH =================
document.getElementById("searchInput").addEventListener("input", async function () {

    const res = await fetch(`${empApi}/search?name=${this.value}`);
    employees = await res.json();

    currentPage = 1;
    renderTable();
});