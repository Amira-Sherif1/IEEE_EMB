// Form validation and password confirmation
function validateForm(formId) {
    const form = document.getElementById(formId);
    if (!form) return;

    const password = form.querySelector('input[type="password"][id$="Password"]');
    const confirmPassword = form.querySelector('input[id="confirmPassword"]');

    form.addEventListener('submit', function (e) {
        let isValid = true;

        // Validate required fields
        const requiredFields = form.querySelectorAll('[required]');
        requiredFields.forEach(field => {
            if (!field.value) {
                isValid = false;
                showError(field, 'This field is required');
            } else {
                clearError(field);
            }
        });

        // Validate password confirmation
        if (password && confirmPassword) {
            if (password.value !== confirmPassword.value) {
                isValid = false;
                showError(confirmPassword, 'Passwords do not match');
            }
        }

        // Validate file upload (if present)
        const fileInput = form.querySelector('input[type="file"]');
        if (fileInput && fileInput.files.length > 0) {
            const file = fileInput.files[0];
            const maxSize = 10 * 1024 * 1024; // 10MB
            const allowedTypes = ['.pdf', '.doc', '.docx'];

            if (file.size > maxSize) {
                isValid = false;
                showError(fileInput, 'File size must be less than 10MB');
            }

            const fileExtension = '.' + file.name.split('.').pop().toLowerCase();
            if (!allowedTypes.includes(fileExtension)) {
                isValid = false;
                showError(fileInput, 'Only PDF and DOC files are allowed');
            }
        }

        if (!isValid) {
            e.preventDefault();
        }
    });

    // Real-time password confirmation validation
    if (password && confirmPassword) {
        confirmPassword.addEventListener('input', function () {
            if (this.value !== password.value) {
                showError(this, 'Passwords do not match');
            } else {
                clearError(this);
            }
        });
    }
}

function showError(field, message) {
    const errorSpan = field.nextElementSibling;
    if (errorSpan && errorSpan.classList.contains('text-red-500')) {
        errorSpan.textContent = message;
    }
}

function clearError(field) {
    const errorSpan = field.nextElementSibling;
    if (errorSpan && errorSpan.classList.contains('text-red-500')) {
        errorSpan.textContent = '';
    }
}

// File upload preview
function setupFileUpload() {
    const fileInput = document.querySelector('input[type="file"]');
    if (!fileInput) return;

    const fileLabel = fileInput.closest('label').querySelector('span');
    const originalText = fileLabel.textContent;

    fileInput.addEventListener('change', function (e) {
        const fileName = e.target.files[0]?.name;
        if (fileName) {
            fileLabel.textContent = `Selected: ${fileName}`;
        } else {
            fileLabel.textContent = originalText;
        }
    });

    // Drag and drop functionality
    const dropZone = fileInput.closest('.border-dashed');

    ['dragenter', 'dragover', 'dragleave', 'drop'].forEach(eventName => {
        dropZone.addEventListener(eventName, preventDefaults, false);
    });

    function preventDefaults(e) {
        e.preventDefault();
        e.stopPropagation();
    }

    ['dragenter', 'dragover'].forEach(eventName => {
        dropZone.addEventListener(eventName, highlight, false);
    });

    ['dragleave', 'drop'].forEach(eventName => {
        dropZone.addEventListener(eventName, unhighlight, false);
    });

    function highlight(e) {
        dropZone.classList.add('border-teal-500', 'bg-teal-50');
    }

    function unhighlight(e) {
        dropZone.classList.remove('border-teal-500', 'bg-teal-50');
    }

    dropZone.addEventListener('drop', handleDrop, false);

    function handleDrop(e) {
        const dt = e.dataTransfer;
        const files = dt.files;
        fileInput.files = files;

        const event = new Event('change', { bubbles: true });
        fileInput.dispatchEvent(event);
    }
}

// Initialize all functionality
document.addEventListener('DOMContentLoaded', function () {
    validateForm('memberForm');
    validateForm('mentorForm');
    setupFileUpload();
});