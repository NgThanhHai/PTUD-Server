<template>
  <div class="register-shipper-container">
    <div class="form-container">
      <div class="avatar">
        <div
          class="thumb image-circle"
          v-bind:style="{
            backgroundImage: 'url(' + getAvatar + ')',
            width: 180 + 'px',
            height: 180 + 'px',
          }"
        ></div>
        <input
          v-show="false"
          ref="inputImage"
          @change="handleChangeFile"
          type="file"
        />
        <button @click="handleChangeImage('avatar')">Upload avatar</button>
      </div>
      <div class="form">
        <div class="form-input">
          <span class="input-label"> Name </span>
          <input v-model="name" />
        </div>
        <div class="form-input">
          <span class="input-label"> Phone </span>
          <input v-model="phone" />
        </div>
        <div class="form-input">
          <span class="input-label"> Address </span>
          <input v-model="address" />
        </div>
        <div class="form-input">
          <span class="input-label"> Indentify </span>
          <input v-model="identify" />
        </div>
        <div class="form-input">
          <span class="input-label"> Negative certification </span>
          <input
            v-show="false"
            ref="inputNegativeCert"
            @change="handleChangeFile"
            type="file"
          />
          <div
            v-if="getNegativeCert"
            class="thumb"
            v-bind:style="{
              backgroundImage: 'url(' + getNegativeCert + ')',
              width: 300 + 'px',
              height: 300 + 'px',
            }"
          ></div>
          <button class="button-upload" @click="handleChangeImage('negative')">
            Upload certification
          </button>
        </div>
        <div class="form-input">
          <span class="input-label"> Vaccine certification </span>
          <input
            v-show="false"
            ref="inputVaccineCert"
            @change="handleChangeFile"
            type="file"
          />
          <div
            v-if="getVaccineCert"
            class="thumb"
            v-bind:style="{
              backgroundImage: 'url(' + getVaccineCert + ')',
              width: 300 + 'px',
              height: 300 + 'px',
            }"
          ></div>
          <button class="button-upload" @click="handleChangeImage('vaccine')">
            Upload certification
          </button>
        </div>
      </div>
    </div>

    <div class="submit"><button class="full-width">Submit</button></div>
  </div>
</template>
<script>
import uploadService from "../api/uploadService.js";
export default {
  name: "RegisterShipper",
  data: () => ({
    id: "",
    name: "",
    avatar: "",
    phone: "",
    address: "",
    negative_cert: "",
    vaccine_cert: "",
    identify: "",

    type: "",
  }),
  computed: {
    defaultAvatar() {
      return require("../assets/avatar-default.svg");
    },
    getAvatar() {
      if (!this.avatar) {
        return this.defaultAvatar;
      }
      return this.avatar;
    },
    getNegativeCert() {
      if (!this.negative_cert) {
        return "";
      }
      return this.negative_cert;
    },
    getVaccineCert() {
      if (!this.vaccine_cert) {
        return "";
      }
      return this.vaccine_cert;
    },
  },
  methods: {
    handleBlur() {},

    handleChangeImage(type) {
      switch (type) {
        case "avatar":
          this.type = "avatar";
          this.$refs.inputImage.value = null;
          this.$refs.inputImage.click();
          break;
        case "negative":
          this.type = "negative";
          this.$refs.inputNegativeCert.value = null;
          this.$refs.inputNegativeCert.click();
          break;
        case "vaccine":
          this.type = "vaccine";
          this.$refs.inputVaccineCert.value = null;
          this.$refs.inputVaccineCert.click();
          break;
        default:
          this.type = "avatar";
          break;
      }
    },
    async handleChangeFile(event) {
      console.log(event.target.files);
      if (event.target.files.length === 0) {
        return;
      }
      const formData = new FormData();
      formData.append("file", event.target.files[0]);
      const rs = await uploadService.uploadToServer(formData);
      if (!rs || !rs.success) {
        return;
      }
      switch (this.type) {
        case "avatar":
          this.avatar = rs.data.path;
          break;
        case "negative":
          this.negative_cert = rs.data.path;
          break;
        case "vaccine":
          this.vaccine_cert = rs.data.path;
          break;
        default:
          break;
      }
    },
  },
};
</script>
<style scoped>
.register-shipper-container {
  display: flex;
  flex-direction: column;
}
.form-input {
  display: flex;
  flex-direction: column;

  margin: 16px 0;
}

.avatar {
  flex-grow: 1;

  display: flex;
  flex-direction: column;
  align-items: center;
}

.form {
  flex-grow: 4;
}

.button-upload {
  max-width: 160px;
}
.full-width {
  width: 100%;
  height: 40px;
}
</style>
