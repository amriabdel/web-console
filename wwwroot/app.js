let allStudents = [];

window.onload = apiGetStudents;

async function apiGetStudents() {
    const response = await fetch('/Student');
    allStudents = await response.json();
    renderTable();
}

function renderTable() {
    const tbody = document.getElementById('studentTableBody');
    tbody.innerHTML = '';

    allStudents.forEach(s => {
        tbody.innerHTML += `
            <tr onclick="uiSelectStudent(${s.id})" style="cursor:pointer">
                <td>${s.name}</td>
                <td><strong>${(s.averageScore || 0).toFixed(1)}%</strong></td>
                <td><button class="btn-edit" style="padding: 5px;">View</button></td>
            </tr>`;
    });
}

async function uiAddStudent() {
    const name = document.getElementById('regName').value;
    const email = document.getElementById('regEmail').value;
    if(!name || !email) return alert("Fill name and email");

    await fetch('/Student', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ name, email })
    });
    apiGetStudents();
}

function uiSelectStudent(id) {
    const student = allStudents.find(s => s.id === id);
    if (!student) return;

    document.getElementById('gradePanel').style.display = 'block';
    document.getElementById('panelTitle').innerText = "Grading: " + student.name;
    document.getElementById('activeStudentId').value = id;

    const list = document.getElementById('historyList');
    
    if (student.grades && student.grades.length > 0) {
        // Find this part inside your uiSelectStudent function and replace the <li> generation:
        list.innerHTML = student.grades.map(g => `
            <li style="display: flex; justify-content: space-between; border-bottom: 1px solid #eee; padding: 5px 0;">
                <span>${g.subject}: <strong>${g.score}%</strong></span>
                <button onclick="event.stopPropagation(); uiDeleteGrade(${g.id}, ${id})" 
                        style="color:red; background:none; border:none; cursor:pointer; font-weight:bold;">✕</button>
            </li>
        `).join('');
    } else {
        list.innerHTML = "<li>No grades yet.</li>";
    }
}

async function uiSubmitGrade() {
    const id = document.getElementById('activeStudentId').value;
    const subject = document.getElementById('courseName').value;
    const score = document.getElementById('courseScore').value;

    if(!score) return alert("Enter a score");

    await fetch(`/Student/${id}/grades`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ subject, score: parseInt(score) })
    });

    document.getElementById('courseScore').value = '';
    await apiGetStudents();
    uiSelectStudent(parseInt(id)); 
}

async function uiDeleteStudent() {
    const id = document.getElementById('activeStudentId').value;
    if (confirm("Delete this student?")) {
        await fetch(`/Student/${id}`, { method: 'DELETE' });
        document.getElementById('gradePanel').style.display = 'none';
        apiGetStudents();
    }
}

async function uiDeleteGrade(gradeId, studentId) {
    if (!confirm("Are you sure you want to remove this grade?")) return;

    try {
        const response = await fetch(`/Student/grades/${gradeId}`, {
            method: 'DELETE'
        });

        if (response.ok) {
            await apiGetStudents();
            uiSelectStudent(studentId);
        } else {
            alert("Failed to delete grade.");
        }
    } catch (error) {
        console.error("Error deleting grade:", error);
    }
}