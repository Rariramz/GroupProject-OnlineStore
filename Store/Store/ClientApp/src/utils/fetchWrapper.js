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

export const getFile = async (url, callback) => {
  try {
    let res = await fetch(url, {
      method: "GET",
    });
    if (res.ok) {
      let reader = await res.body.getReader();
      let base64Chunks = [];
      let done;
      let value;
      while (!done) {
        ({ value, done } = await reader.read());
        if (done) {
          callback(
            `data:image/png;base64,${Buffer.from(
              base64Chunks.join().split(",")
            ).toString("base64")}`
          );
        }
        base64Chunks.push(value);
      }
    }
  } catch (e) {
    console.error(e);
  }
};

export const getFileBase64 = async (url, callback) => {
  try {
    let res = await fetch(url, {
      method: "GET",
    });
    if (res.ok) {
      let json;
      json = await res.body
        .getReader()
        .read()
        .then(({ value }) =>
          callback(
            `data:image/png;base64,${Buffer.from(value).toString("base64")}`
          )
        );
    }
  } catch (e) {
    console.error(e);
  }
};
