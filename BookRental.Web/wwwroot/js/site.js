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

window.apiRequest = function(url, options = {}) {
    const defaults = {
        method: 'GET',
        data: null,
        showError: true,
        successMessage: null,
        onSuccess: null,
        onError: null
    };

    const config = { ...defaults, ...options };

    const ajaxSettings = {
        url: url,
        method: config.method,
        success: function(result) {
            if (config.successMessage) {
                showAlert(config.successMessage, 'success');
            }

            if (config.onSuccess) {
                config.onSuccess(result);
            }
        },
        error: function(xhr, status, error) {
            console.error(`API ${config.method} Error:`, error);

            let errorMessage = window.localizedStrings?.requestFailed || 'Request failed';

            if (xhr.responseJSON && xhr.responseJSON.error) {
                errorMessage = xhr.responseJSON.error;
            } else if (xhr.responseText) {
                try {
                    const errorData = JSON.parse(xhr.responseText);
                    if (errorData.error) {
                        errorMessage = errorData.error;
                    }
                } catch (e) {
                    showAlert(e, 'danger');
                }
            }

            if (config.showError !== false) {
                showAlert(errorMessage, 'danger');
            }

            if (config.onError) {
                config.onError(error);
            }
        }
    };

    if (config.data && (config.method === 'POST' || config.method === 'PUT' || config.method === 'PATCH')) {
        if (config.data instanceof FormData) {
            ajaxSettings.data = config.data;
            ajaxSettings.processData = false;
            ajaxSettings.contentType = false;
        } else {
            ajaxSettings.data = config.data;
        }
    }

    return $.ajax(ajaxSettings);
};

window.openModal = function(partialUrl, options = {}) {
    const defaults = {
        modalSize: 'modal-xl',
        onSuccess: null,
        onError: null
    };

    const config = { ...defaults, ...options };

    $.ajax({
        url: partialUrl,
        method: 'GET',
        success: function(html) {
            const tempDiv = document.createElement('div');
            tempDiv.innerHTML = html;
            const form = tempDiv.querySelector('form');

            if (!form) {
                showAlert(window.localizedStrings?.noFormFound || 'No form found in modal content', 'danger');
                return;
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
        },
        error: function(xhr, status, error) {
            console.error('Error opening modal:', error);
            showAlert(window.localizedStrings?.errorLoadingForm || 'Error loading form', 'danger');

            if (config.onError) {
                config.onError(error);
            }
        }
    });
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
    }).always(() => {
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

function initializeDataTable(tableId, config) {
    const defaultConfig = {
        processing: true,
        serverSide: false,
        pageLength: 25,
        lengthMenu: [[10, 25, 50, 100], [10, 25, 50, 100]],
        order: [[0, 'asc']]
    };
    return $(tableId).DataTable(Object.assign({}, defaultConfig, config));
}