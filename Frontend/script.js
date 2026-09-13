const API = "http://localhost:5055/api/chat";

let conversacionId = null;

const messages = document.getElementById("messages");
const input = document.getElementById("messageInput");
const form = document.getElementById("chatForm");
const newChat = document.getElementById("newChat");
const conversationList = document.getElementById("conversationList");

function agregarMensaje(texto, tipo) {
    const div = document.createElement("div");
    div.className = `message ${tipo}`;
    div.textContent = texto;
    messages.appendChild(div);
    messages.scrollTop = messages.scrollHeight;
}

function limpiarChat() {
    messages.innerHTML = "";
}

async function enviarMensaje(pregunta) {
    const response = await fetch(API, {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify({
            pregunta,
            conversacionId
        })
    });

    if (!response.ok) {
        throw new Error("Error al comunicarse con Lion-IA");
    }

    return await response.json();
}

form.addEventListener("submit", async (e) => {
    e.preventDefault();

    const pregunta = input.value.trim();

    if (!pregunta) return;

    agregarMensaje(pregunta, "user");
    input.value = "";
    input.disabled = true;

    try {
        const data = await enviarMensaje(pregunta);

        conversacionId = data.conversacionId;

        agregarMensaje(data.respuesta, "ai");

        cargarConversaciones();
    } catch (error) {
        agregarMensaje("Error al conectar con Lion-IA.", "ai");
    }

    input.disabled = false;
    input.focus();
});

newChat.addEventListener("click", () => {
    conversacionId = null;
    limpiarChat();
    input.focus();
});

async function cargarConversacion(id) {
    const response = await fetch(`${API}/${id}`);

    if (!response.ok) return;

    const data = await response.json();

    conversacionId = data.conversacionId;

    limpiarChat();

    data.mensajes.forEach(mensaje => {
        agregarMensaje(mensaje.pregunta, "user");
        agregarMensaje(mensaje.respuesta, "ai");
    });
}

async function cargarConversaciones() {
    conversationList.innerHTML = "";

    if (conversacionId !== null) {
        const div = document.createElement("div");

        div.className = "conversation";
        div.textContent = `Conversación ${conversacionId}`;

        div.addEventListener("click", () => {
            cargarConversacion(conversacionId);
        });

        conversationList.appendChild(div);
    }
}

cargarConversaciones();
