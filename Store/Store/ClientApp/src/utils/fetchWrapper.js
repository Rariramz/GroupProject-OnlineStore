export const get = async (url, callback) => {
    fetchWrapper("GET", url, callback);
};
export const post = async (url, callback, obj) => {
    fetchWrapper("POST", url, callback, obj);
};

const fetchWrapper = async (method, url, callback, obj) => {
<<<<<<< HEAD
    try {
        let formData = new FormData();
        Object.entries(obj).forEach((element) => {
            formData.append(element[0], element[1]);
        });
=======
  try {
    let formData = new FormData();
    if (obj) {
      Object.entries(obj).forEach((element) => {
        formData.append(element[0], element[1]);
      });
    }
>>>>>>> develop

        let res = await fetch(url, {
            method,
            body: formData,
        });
        if (res.ok) {
            const json = await res.json();
            callback(json);
        }
    } catch (e) {
        console.error(e);
    }
};