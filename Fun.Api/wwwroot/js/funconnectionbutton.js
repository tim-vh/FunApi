class FunConnectionButton extends HTMLElement {

    get connectedClass() { return this.getAttribute("connectedclass"); }
    set connectedClass(value) { this.setAttribute("connectedclass", value); }

    get disconnectedClass() { return this.getAttribute("disconnectedclass"); }
    set disconnectedClass(value) { this.setAttribute("disconnectedclass", value); }

    connectedCallback() {
        if (!this.connectedClass) this.connectedClass = "connected";
        if (!this.disconnectedClass) this.disconnectedClass = "disconnected";

        this.button = this.querySelector("button")
        this.button.textContent = "Connect";
        this.button.classList.add(this.disconnectedClass);
    }

    setConnected() {
        this.button.classList.add(this.connectedClass);
        this.button.classList.remove(this.disconnectedClass);
        this.button.textContent = "Disconnect";
    }

    setDisconnected() {
        this.button.classList.add(this.disconnectedClass);
        this.button.classList.remove(this.connectedClass);
        this.button.textContent = "Connect";
    }
}

window.customElements.define("fun-connection-button", FunConnectionButton);