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
window.apiGet = async function(url, options = {}) {
    try {
        const response = await fetch(url);
        if (!response.ok) {
            const error = await response.json().catch(() => ({ error: 'Request failed' }));
            throw new Error(error.error || 'Request failed');
        }
        const data = await response.json();

        if (options.onSuccess) {
            options.onSuccess(data);
        }

        return data;
    } catch (error) {
        console.error('API GET Error:', error);
        if (options.showError !== false) {
            showAlert(error.message, 'danger');
        }
        if (options.onError) {
            options.onError(error);
        }
        throw error;
    }
};

window.apiPost = async function(url, data, options = {}) {
    try {
        const response = await fetch(url, {
            method: 'POST',
            body: data
        });
        if (!response.ok) {
            const error = await response.json().catch(() => ({ error: 'Request failed' }));
            throw new Error(error.error || 'Request failed');
        }
        const result = await response.json();

        if (options.successMessage) {
            showAlert(options.successMessage, 'success');
        }

        if (options.onSuccess) {
            options.onSuccess(result);
        }

        return result;
    } catch (error) {
        console.error('API POST Error:', error);
        if (options.showError !== false) {
            showAlert(error.message, 'danger');
        }
        if (options.onError) {
            options.onError(error);
        }
        throw error;
    }
};

window.apiDelete = async function(url, options = {}) {
    try {
        const response = await fetch(url, {
            method: 'POST'
        });
        if (!response.ok) {
            const error = await response.json().catch(() => ({ error: 'Request failed' }));
            throw new Error(error.error || 'Request failed');
        }
        const result = await response.json();

        if (options.successMessage) {
            showAlert(options.successMessage, 'success');
        }

        if (options.onSuccess) {
            options.onSuccess(result);
        }

        return result;
    } catch (error) {
        console.error('API DELETE Error:', error);
        if (options.showError !== false) {
            showAlert(error.message, 'danger');
        }
        if (options.onError) {
            options.onError(error);
        }
        throw error;
    }
};