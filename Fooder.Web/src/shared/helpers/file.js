export function getFileExtension(filename) {
  return filename
    .slice(((filename.lastIndexOf(".") - 1) >>> 0) + 2)
    .toLowerCase();
}

export function prepareFormData(photos) {
  const formData = new FormData();
  photos.forEach((photo, index) => {
    formData.append(`files[${index}]`, photo);
  });
  return formData;
}
