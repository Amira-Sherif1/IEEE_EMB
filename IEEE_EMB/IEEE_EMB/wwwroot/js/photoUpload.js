const uploadArea = document.getElementById('uploadArea');
const fileInput = document.getElementById('fileInput');
const preview = document.getElementById('preview');
const saveBtn = document.getElementById('saveBtn');
const previewContainer = document.querySelector('.preview-container');

// Drag and drop handlers
uploadArea.addEventListener('dragover', (e) => {
    e.preventDefault();
    uploadArea.classList.add('dragover');
});

['dragleave', 'dragend'].forEach(type => {
    uploadArea.addEventListener(type, (e) => {
        e.preventDefault();
        uploadArea.classList.remove('dragover');
    });
});

uploadArea.addEventListener('drop', (e) => {
    e.preventDefault();
    uploadArea.classList.remove('dragover');
    const file = e.dataTransfer.files[0];
    handleFile(file);
});

// File input change handler
fileInput.addEventListener('change', (e) => {
    const file = e.target.files[0];
    handleFile(file);
});

// Handle the selected file
function handleFile(file) {
    if (!file || !file.type.startsWith('image/')) {
        alert('Please select an image file.');
        return;
    }

    const reader = new FileReader();
    reader.onload = (e) => {
        preview.src = e.target.result;
        previewContainer.classList.add('loaded');
        saveBtn.disabled = false;
    };
    reader.readAsDataURL(file);
}

// Click anywhere in upload area to trigger file input
uploadArea.addEventListener('click', (e) => {
    if (e.target.tagName !== 'BUTTON') {
        fileInput.click();
    }
});

// Save button handler
saveBtn.addEventListener('click', () => {
    // Here you would typically send the image to your server
    // For now, we'll just show a success message
    saveBtn.textContent = 'Saving...';
    setTimeout(() => {
        alert('Profile photo updated successfully!');
        window.history.back();
    }, 1000);
});