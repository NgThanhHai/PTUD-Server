<template>
  <div class="register-shipper-container">
    <div class="form-container">
      <form @submit.prevent="SubmitData">
        <h1>Báo cáo sự cố</h1>
        <label>Order_id</label>
        <input
          type="text"
          name="order_id"
          v-model="trouble.order_id"
          placeholder="Mã đơn"
        />
        <br />
        <br />
        <label>Description</label>
        <input
          type="text"
          name="description"
          v-model="trouble.description"
          placeholder="Mô tả sự cố"
        />
        <br />
        <br />
        <div class="submit">
          <button type="submit" class="full-width">Submit</button>
        </div>
      </form>
    </div>
  </div>
</template>
<script>
import TroubleService from "@/api/TroubleService";

export default {
  name: "SubmitTrouble",
  data() {
    return {
      trouble: {
        id: "61b0ce85536c31dd0b778980",
        order_id: "",
        shipper_id: "61b0ce9e536c31dd0b778972",
        created_at: "2019-12-31T17:00:00.000+00:00",
        updated_at: "2020-12-31T17:00:00.000+00:00",
        description: "",
        status: 1,
      },
    };
  },
  methods: {
    SubmitData() {
      TroubleService.create(this.trouble);
      console.log(this.trouble);
    },
  },
};
</script>
