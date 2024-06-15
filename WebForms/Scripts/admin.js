// Restricts input for the given textbox to the given inputFilter function.
// Adaptado de https://stackoverflow.com/a/469362/10302170
function setInputFilter(textbox, inputFilter, errMsg, cssIndicator) {
    ["input", "keydown", "keyup", "mousedown", "mouseup", "select", "contextmenu", "drop", "focusout"].forEach(function (event) {
        textbox.addEventListener(event, function (e) {
            if (inputFilter(this.value)) {
                // Accepted value.
                if (["keydown", "mousedown", "focusout"].indexOf(e.type) >= 0) {
                    this.classList.remove("input-error");
                    this.setCustomValidity("");
                }

                this.oldValue = this.value;
                this.oldSelectionStart = this.selectionStart;
                this.oldSelectionEnd = this.selectionEnd;
            }
            else if (this.hasOwnProperty("oldValue")) {
                // Rejected value: restore the previous one.
                if(cssIndicator) this.classList.add("input-error");
                this.setCustomValidity(errMsg);
                this.reportValidity();
                this.value = this.oldValue;
                this.setSelectionRange(this.oldSelectionStart, this.oldSelectionEnd);
            }
            else {
                // Rejected value: nothing to restore.
                this.value = "";
            }
        });
    });
}

// Esta función llama a setInputFilter que agrega eventListeners
// para los controles que requieran validación numérica y lanza un mensaje
// cuando se ingresen caracteres inválidos
// El parametro cssIndicator habilita opcionalmente que se agregue la clase
// CSS 'input-error' para aplicar un estilo al control
const bindNumberValidation = (elementId, cssIndicator = false) => {
    setInputFilter(document.getElementById(elementId), function (value) {
        return /^\d*(?:[.,]\d*)?$/.test(value); // Solo números y '.', RegExp.
    }, 'Solo se permiten números y "," o "."');
};

// Crea una nueva instancia Modal para el MasterModal
const masterModal = new bootstrap.Modal(document.getElementById('MasterModal'));

// Crea una nueva instancia de Bootstrap Toast para las notificaciones Toast
const masterToastElement = document.getElementById('masterToast');
const masterToast = new bootstrap.Toast(masterToastElement);