class FunVideoButton extends HTMLElement {

    get thumbnailUrl() { return this.getAttribute("thumbnailurl"); }
    set thumbnailUrl(value) { this.setAttribute("thumbnailurl", value); }

    get apiBaseUrl() { return this.getAttribute("apiBaseUrl"); }
    set apiBaseUrl(value) { this.setAttribute("apiBaseUrl", value); }

    get videoUrl() { return this.getAttribute("videourl"); }
    set videoUrl(value) { this.setAttribute("videourl", value); }

    get title() { return this.getAttribute("title"); }
    set title(value) { this.setAttribute("title", value); }

    connectedCallback() {
        this.apiBaseUrl = this.apiBaseUrl ?? "";

        this.button = this.querySelector("button");
        this.button.style.backgroundImage = `url('${this.thumbnailUrl}')`;
        this.button.onclick = this.videoButton_onclick;

        this.titleBar = this.querySelector("span");
        this.titleBar.textContent = this.title;
    }

    videoButton_onclick = () => {
        this.sendPlayVideoRequest(this.videoUrl);
    }

    async sendPlayVideoRequest(videoUrl) {
        let url = this.apiBaseUrl + "api/video/play/" + encodeURIComponent(videoUrl);
        await fetch(url);
    }
}

window.customElements.define("fun-video-button", FunVideoButton);