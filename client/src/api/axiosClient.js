import axios from "axios";

const axiosClient = axios.create({
  baseURL: "https://d2h-backend-server.herokuapp.com",
  headers: {
    "Content-Type": "application/json",
  },
  params: {},
});

// Add a request interceptor


// Add a response interceptor
axiosClient.interceptors.response.use(
  function (response) {
    // Any status code that lie within the range of 2xx cause this function to trigger
    // Do something with response data
    return response.data;
  },
  function (error) {
    //parse error
    // const { config, status, data } = error.response;
    return Promise.reject(error.response.data);
  }
);

export default axiosClient;
