export const get = async (url, callback) => {
  fetchWrapper("GET", url, callback);
};
export const post = async (url, callback, obj) => {
  fetchWrapper("POST", url, callback, obj);
};

const fetchWrapper = async (method, url, callback, obj) => {
  try {
    let formData = new FormData();
    let res;
    if (obj) {
      Object.entries(obj).forEach((element) => {
        formData.append(element[0], element[1]);
      });

      res = await fetch(url, {
        method,
        body: formData,
      });
    } else {
      console.log(method, url);
      res = await fetch(url, {
        method,
      });
    }
    if (res.ok) {
      let json;
      json = await res.json();
      callback(json);
    }
  } catch (e) {
    console.error(e);
  }
};
