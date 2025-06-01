baseUrl = "https://localhost:7149";
selectedId = null;

async function loadTodos() {
    const response = await fetch(baseUrl + '/api/TodoList/items');

    if (!response.ok) {
        throw new Error(`Error HTTP: ${response.status}`);
    }

    const items = await response.json(); // ← Espera JSON de la API

    const tbody = document.getElementById('todo-table');
    if (!tbody) return; // No hacer nada si no existe el tbody

    tbody.innerHTML = ''; // Limpiar contenido previo

    if (!Array.isArray(items) || items.length === 0) {
        tbody.innerHTML = '<tr><td colspan="5">No hay items</td></tr>';
        return;
    }
    tbody.innerHTML = `
        <tr>
            <th>ID</th>
            <th>Título</th>
            <th>Descripción</th>
            <th>Categoría</th>
            <th>Completado</th>
            <th>Acciones</th>
        </tr>
    `;

    items.forEach(item => {
            
        tbody.innerHTML += `
            <tr onclick="selectRow('${item.id}', '${item.title}', '${item.description}', '${item.category}')">
                <td>${item.id}</td>
                <td>${item.title}</td>
                <td>${item.description}</td>
                <td>${item.category}</td>
                <td>${item.isCompleted ? 'Sí' : 'No'}</td>
                <td>
                    <button class="actionButton" onclick="event.stopPropagation(); editItem(${item.id}, '${item.title}', '${item.description}', '${item.category}')">✏️</button>
                    <button class="actionButton" onclick="event.stopPropagation(); deleteItem(${item.id})" class="btn-danger">🗑️</button>
                </td>
            </tr>
        `;

    });
}

function selectRow(id, title, description, category) {
    selectedId = id;
    document.getElementById('edit-id').value = id;
    document.getElementById('title').value = title;
    document.getElementById('description').value = description;
    document.getElementById('category').value = category;
}

function editItem(id, title, desc, category) {
    document.getElementById('edit-id').value = id;
    document.getElementById('title').value = title;
    document.getElementById('description').value = desc;
    document.getElementById('category').value = category;
    selectedId = id;
}

async function saveTodo() {
    const id = document.getElementById('edit-id').value;
    const title = document.getElementById('title').value;
    const description = document.getElementById('description').value;
    const category = document.getElementById('category').value;

    if (id) {
        var response = await fetch(baseUrl + `/api/TodoList/update/${id}?description=${encodeURIComponent(description)}`, { method: 'PUT' });
        if (response.ok) {
            alert("Actualizado");
        } else {
            alert("Hay errores en la conexión");
        }
    } else {
        const response = await fetch(baseUrl + `/api/TodoList/additem?title=${encodeURIComponent(title)}&description=${encodeURIComponent(description)}&category=${encodeURIComponent(category)}`, { method: 'POST' });

        if (response.ok) {
            alert("Creado");
        } else {
            alert("Hay errores en la conexión");
        }
    }

    clearSelection();

    loadTodos();
}

function clearSelection() {
    document.getElementById('edit-id').value = '';
    document.getElementById('title').value = '';
    document.getElementById('description').value = '';
    document.getElementById('category').value = '';
    selectedId = null;
}

async function deleteItem(id) {
    const res = await fetch(baseUrl + `/api/TodoList/${id}`, { method: 'DELETE' });

    if (res.ok) {
        alert("Eliminado");
        loadTodos();
    } else {
        alert(await res.text());
    }
}

function selectItem(item) {
    selectedId = item.id;
    const container = document.getElementById('progression-container');
    container.innerHTML = '';

    let total = 0;

    if (item.progressions === undefined) {
        return;
    }

    item.progressions.forEach(p => {
        total += p.percent;
        const div = document.createElement('div');
        div.innerHTML = `
            <p>${new Date(p.date).toLocaleDateString()} - ${total}%</p>
            <div class="bar"><div style="width:${total}%">${total}%</div></div>
        `;
        container.appendChild(div);
    });
}

async function addProgression() {
    if (!selectedId) return alert("Selecciona un item primero");

    const date = document.getElementById('progress-date').value;
    const percent = document.getElementById('progress-percent').value;

    const res = await fetch(baseUrl + `/api/TodoList/progress/${selectedId}?date=${date}&percent=${percent}`, { method: 'POST' });
    if (res.ok) {
        alert("Progreso añadido");
        loadTodos();
    } else {
        alert(await res.text());
    }
}

async function clearITems() {
    const res = await fetch(baseUrl + `/api/TodoList/clearitems`, { method: 'POST' });

    if (res.ok) {
        loadTodos();
    } else {
        alert(await res.text());
    }
}

loadTodos();