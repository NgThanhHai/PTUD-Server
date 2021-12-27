import axiosClient from "./axiosClient";

const url =
  "/api/upload/cloudinary-upload";
const uploadService = {
  uploadToServer(fileToUpload) {
    return axiosClient.post(url, fileToUpload);
  },
};

export default uploadService;
