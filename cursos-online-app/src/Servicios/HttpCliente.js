import axios from 'axios';

//axios.defaults.baseURL = 'http://localhost:5000/api';
axios.defaults.baseURL = 'https://stagingapiswork.azurewebsites.net/api';

axios.interceptors.request.use((config) => {
    const token_seguridad = window.localStorage.getItem('token_seguridad');
    if (token_seguridad) {
        config.headers.Authorization = 'Bearer ' + token_seguridad;
        return config;
    }
}, error => {
    return Promise.reject(error);
});


const requestGenerico = {
    get: (url) => axios.get(url),
    post: (url, body) => axios.post(url, body),
    put: (url, body) => axios.put(url, body),
    delete: (url) => axios.delete(url)
};

export default requestGenerico