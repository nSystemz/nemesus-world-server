let settings = {
    timeStamp: true,
    removeInputColors: true,
    characterCount: true,
    lowerCaseCommand: true,
    scrollbar: true,
    maxLength: 200
}

let CHAT_BOX, MESSAGE_LIST, CHAT_INPUT, CHAR_COUNT;

let chatActive = true;
let chatInputStatus = false;

const inputHistory = [];
let inputHistoryPosition = -1;
let inputCache = "";

const chatAPI = {
    clear: () => {
        MESSAGE_LIST.innerHTML = "";
    },

    timestamp: (modus) => {
        settings.timeStamp = modus;
    },

    fontsize: (size) => {
        const style = document.createElement('style');
        style.innerHTML = `
          * {
            font-family: Ubuntu, sans-serif;
            font-weight: 700;
            font-size: ${size}vh;
            background-color: transparent;
            user-select: none;
          }
        `;
        document.head.appendChild(style);
    },

    highlight: () => {
        if (chatActive)
            MESSAGE_LIST.scrollTop = MESSAGE_LIST.scrollHeight;
    },

    push: (text) => {
        if (text.length < 1) return;

        MESSAGE_LIST.innerHTML += `
        <div class="message stroke">
            ${settings.timeStamp ? `<span class="timeStamp">${getDateString()}</span>` : ""}
            <span>${text}</span>
        </div>`;

        MESSAGE_LIST.childElementCount > 100 && MESSAGE_LIST.firstElementChild.remove();
        MESSAGE_LIST.scrollTop = MESSAGE_LIST.scrollHeight;
    },

    activate: (toggle) => {
        if (!toggle && chatActive)
            setChatInputStatus(false);
        chatActive = toggle;
    },

    show: (toggle) => {
        if (!toggle && chatInputStatus)
            setChatInputStatus(false);

        toggle ? CHAT_BOX.className = "chatBox" : CHAT_BOX.className = "hide";

        chatActive = toggle;
    }
}

if (typeof mp !== 'undefined') {
    const api = {
        "chat:push": chatAPI.push,
        "chat:clear": chatAPI.clear,
        "chat:activate": chatAPI.activate,
        "chat:show": chatAPI.show
    };

    for (const fn in api) {
        mp.events.add(fn, api[fn]);
    }
}

const setChatInputStatus = (status) => {
    if ((!chatActive && status) || (status == chatInputStatus))
        return;

    mp.invoke("focus", status);
    mp.invoke("setTypingInChatState", status);

    if (status) {
        chatInputStatus = true;
        CHAT_INPUT.className = "inputBar";
        if (settings.characterCount)
            CHAR_COUNT.className = "charCount stroke";
        CHAT_INPUT.focus();
    } else {
        chatInputStatus = false;
        CHAT_INPUT.className = "hide";
        CHAR_COUNT.className = "hide";
    }
}

const getDateString = () => {
    const date = new Date();
    const h = "0" + date.getHours().toString();
    const m = "0" + date.getMinutes().toString();
    const s = "0" + date.getSeconds().toString();
    return `[${h.substr(h.length-2)}:${m.substr(m.length-2)}:${s.substr(s.length-2)}]`;
}

String.prototype.lowerCaseFirstWord = function () {
    const word = this.split(" ")[0];
    return this.replace(new RegExp(word, "gi"), word.toLowerCase());
}

const updateCharCount = () => {
    CHAR_COUNT.innerText = `${CHAT_INPUT.value.length}/${settings.maxLength}`;
}

const sendInput = () => {
    let message = CHAT_INPUT.value.trim();

    if (settings.removeInputColors)
        message = message.replace(/(?=!{).*(?<=})/g, "");

    if (message.length < 1) {
        setChatInputStatus(false);
        return;
    }

    if (message[0] == "/") {
        if (message.length < 2) {
            setChatInputStatus(false);
            return;
        }
        mp.invoke("command", settings.lowerCaseCommand ? message.lowerCaseFirstWord().substr(1) : message.substr(1));
    } else {
        mp.invoke("chatMessage", message);
    }

    inputHistory.unshift(message);
    inputHistory.length > 100 && inputHistory.pop();
    CHAT_INPUT.value = "";
    inputHistoryPosition = -1;
    CHAR_COUNT.innerText = `0/${settings.maxLength}`;
    setChatInputStatus(false);
}

const onArrowUp = () => {
    if (inputHistoryPosition == inputHistory.length - 1)
        return;

    if (inputHistoryPosition == -1)
        inputCache = CHAT_INPUT.value;

    inputHistoryPosition++;
    CHAT_INPUT.value = inputHistory[inputHistoryPosition];
}

const onArrowDown = () => {
    if (inputHistoryPosition === -1)
        return;

    if (inputHistoryPosition === 0) {
        CHAT_INPUT.value = inputCache;
        inputHistoryPosition = -1;
        return;
    }

    inputHistoryPosition--;
    CHAT_INPUT.value = inputHistory[inputHistoryPosition];
}

const onDocumentReady = () => {
    CHAT_BOX = document.getElementById("chatbox");
    MESSAGE_LIST = document.getElementById("messageslist");
    CHAT_INPUT = document.getElementById("chatinput");
    CHAR_COUNT = document.getElementById("charCount");
    CHAT_INPUT.oninput = updateCharCount;
    CHAT_INPUT.maxLength = settings.maxLength;

    if (settings.scrollbar) {
        MESSAGE_LIST.style.overflowY = "auto"
    }

    updateCharCount();

    document.addEventListener("keydown", (e) => {
        switch (e.which) {
            case 84:
                if (!chatInputStatus && chatActive) {
                    setChatInputStatus(true);
                    e.preventDefault();
                }
                break;

            case 13:
                if (chatInputStatus)
                    sendInput();
                break;

            case 38:
                if (chatInputStatus) {
                    onArrowUp();
                    updateCharCount();
                    e.preventDefault();
                }
                break;

            case 40:
                if (chatInputStatus) {
                    onArrowDown();
                    updateCharCount();
                    e.preventDefault();
                }
                break;

            case 33:
                MESSAGE_LIST.scrollTop -= 15;
                break;

            case 34:
                MESSAGE_LIST.scrollTop += 15;
                break;

            case 27:
                if (chatInputStatus && chatActive) {
                    setChatInputStatus(false);
                    e.preventDefault();
                }
                break;
        }
    });
}

document.addEventListener('DOMContentLoaded', onDocumentReady);