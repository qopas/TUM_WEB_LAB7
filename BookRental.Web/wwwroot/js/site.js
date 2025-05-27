window.showLoading = function() {
    document.getElementById('loadingOverlay').classList.remove('hidden');
    document.getElementById('loadingOverlay').classList.add('flex');
};

window.hideLoading = function() {
    document.getElementById('loadingOverlay').classList.add('hidden');
    document.getElementById('loadingOverlay').classList.remove('flex');
};

window.showAlert = function(message, type = 'info') {
    const toastId = 'toast-' + Date.now();
    const iconClass = type === 'success' ? 'check-circle' :
        type === 'danger' ? 'exclamation-triangle' :
            type === 'warning' ? 'exclamation-circle' : 'info-circle';

    const toastHtml = `
                <div id="${toastId}" class="bg-white border border-gray-200 rounded-lg shadow-lg p-4 min-w-80 transform transition-all duration-300 translate-x-full opacity-0">
                    <div class="flex items-center">
                        <div class="flex-shrink-0">
                            <i class="fas fa-${iconClass} text-${type === 'success' ? 'green' : type === 'danger' ? 'red' : type === 'warning' ? 'yellow' : 'blue'}-500"></i>
                        </div>
                        <div class="ml-3 flex-1">
                            <p class="text-sm font-medium text-gray-900">${message}</p>
                        </div>
                        <button onclick="removeToast('${toastId}')" class="ml-4 flex-shrink-0 text-gray-400 hover:text-gray-600">
                            <i class="fas fa-times"></i>
                        </button>
                    </div>
                </div>
            `;

    const container = document.getElementById('toastContainer');
    container.insertAdjacentHTML('beforeend', toastHtml);

    const toast = document.getElementById(toastId);
    setTimeout(() => {
        toast.classList.remove('translate-x-full', 'opacity-0');
        toast.classList.add('translate-x-0', 'opacity-100');
    }, 100);

    setTimeout(() => {
        removeToast(toastId);
    }, 5000);
};

window.removeToast = function(toastId) {
    const toast = document.getElementById(toastId);
    if (toast) {
        toast.classList.add('translate-x-full', 'opacity-0');
        setTimeout(() => {
            toast.remove();
        }, 300);
    }
};

window.apiRequest = async function(url, options = {}) {
    const defaults = {
        method: 'GET',
        data: null,
        showError: true,
        successMessage: null,
        onSuccess: null,
        onError: null
    };

    const config = { ...defaults, ...options };

    try {
        const fetchOptions = {
            method: config.method,
        };

        if (config.data && (config.method === 'POST' || config.method === 'PUT' || config.method === 'PATCH')) {
            fetchOptions.body = config.data;
        }

        const response = await fetch(url, fetchOptions);

        if (!response.ok) {
            const error = await response.json().catch(() => ({
                error: window.localizedStrings?.requestFailed || 'Request failed'
            }));
            throw new Error(error.error || window.localizedStrings?.requestFailed || 'Request failed');
        }

        const result = await response.json();

        if (config.successMessage) {
            showAlert(config.successMessage, 'success');
        }

        if (config.onSuccess) {
            config.onSuccess(result);
        }

        return result;
    } catch (error) {
        console.error(`API ${config.method} Error:`, error);
        if (config.showError !== false) {
            showAlert(error.message, 'danger');
        }
        if (config.onError) {
            config.onError(error);
        }
        throw error;
    }
};

window.openModal = async function(partialUrl, options = {}) {
    const defaults = {
        modalSize: 'modal-xl',
        onSuccess: null,
        onError: null
    };

    const config = { ...defaults, ...options };

    try {
        const response = await fetch(partialUrl);
        if (!response.ok) {
            throw new Error(window.localizedStrings?.failedToLoadModal || 'Failed to load modal content');
        }

        const html = await response.text();
        const tempDiv = document.createElement('div');
        tempDiv.innerHTML = html;
        const form = tempDiv.querySelector('form');

        if (!form) {
            throw new Error(window.localizedStrings?.noFormFound || 'No form found in modal content');
        }

        const formAction = form.dataset.action;
        const operation = form.dataset.operation;
        const entity = form.dataset.entity;
        const modalTitle = operation === 'edit' ?
            `${window.localizedStrings?.edit || 'Edit'} ${entity}` :
            `${window.localizedStrings?.addNew || 'Add New'} ${entity}`;

        const modalHtml = `
            <div class="modal fade" id="dynamicModal" tabindex="-1" aria-labelledby="dynamicModalLabel" aria-hidden="true">
                <div class="modal-dialog ${config.modalSize}">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" id="dynamicModalLabel">${modalTitle}</h5>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="${window.localizedStrings?.close || 'Close'}"></button>
                        </div>
                        <div class="modal-body">
                            ${html}
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">${window.localizedStrings?.cancel || 'Cancel'}</button>
                            <button type="button" class="btn btn-primary" id="modalSubmitBtn">
                                ${window.localizedStrings?.save || 'Save'} ${entity}
                            </button>
                        </div>
                    </div>
                </div>
            </div>
        `;

        $('#dynamicModal').remove();
        $('body').append(modalHtml);

        $('#modalSubmitBtn').on('click', function() {
            submitModalForm(formAction, operation, entity, config.onSuccess);
        });

        $('#dynamicModal').modal('show');

    } catch (error) {
        console.error('Error opening modal:', error);
        showAlert(window.localizedStrings?.errorLoadingForm || 'Error loading form', 'danger');
    }
};

function submitModalForm(formAction, operation, entity, onSuccess) {
    const form = document.querySelector('#dynamicModal form');

    if (!validateForm(form)) {
        return;
    }

    const formData = new FormData(form);
    const submitBtn = document.getElementById('modalSubmitBtn');
    const originalText = submitBtn.innerHTML;

    submitBtn.innerHTML = window.localizedStrings?.saving || 'Saving...';
    submitBtn.disabled = true;

    const successMessage = operation === 'edit' ?
        `${entity} ${window.localizedStrings?.updatedSuccessfully || 'updated successfully!'}` :
        `${entity} ${window.localizedStrings?.createdSuccessfully || 'created successfully!'}`;

    apiRequest(formAction, {
        method: 'POST',
        data: formData,
        successMessage: successMessage,
        onSuccess: () => {
            $('#dynamicModal').modal('hide');
            if (onSuccess) {
                onSuccess();
            }
        }
    }).finally(() => {
        submitBtn.innerHTML = originalText;
        submitBtn.disabled = false;
    });
}

function validateForm(form) {
    if (form.checkValidity()) {
        return true;
    }

    form.classList.add('was-validated');
    return false;
}
function changeCulture(culture) {
    const cookieValue = `c=${culture}|uic=${culture}`;
    document.cookie = `.AspNetCore.Culture=${cookieValue}; path=/; max-age=31536000`;
    window.location.reload();
}