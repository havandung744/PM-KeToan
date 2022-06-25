<template>
  <div class="dropdown">
    <div>
      <button @click="btnOnReplication">Nhân bản</button>
    </div>
    <div>
      <button @click="btnOnDelete">Xóa</button>
    </div>
    <div>
      <button @click="btnOnStopUsing">Ngừng sử dụng</button>
    </div>
  </div>
</template>

<script>

export default {
  props: ["employeeCodeForDelete", "employeeIdSelected"],
  methods: {
    /**
     * Thực hiện hiển thị form xác nhận xóa
     * Author: HVDUNG (25/06/2022)
     */
    async btnOnDelete() {
      let noticeDialog = document.getElementsByClassName("d-dialog-box-delete")[0];
      let bodyTextNoticeDialog = noticeDialog.querySelector(".d-dialog-box-delete .d-dialog-body");
      bodyTextNoticeDialog.innerHTML = `Bạn có muốn xóa Nhân viên &#60;${this.employeeCodeForDelete}&#62; không ?`;
      document.getElementsByClassName("d-dialog-box-delete")[0].style.display =
        "block";
      document.getElementsByClassName("d-dialog-box-delete")[0].style.display =
        "block";
      // ẩn form dropdown
      this.$emit("isShowDropDown");
    },

    /**
     * Thực hiện hiển thị form thông báo
     * Author: HVDUNG (05/06/2022)
     */
    btnOnReplication() {
      let noticeDialog = document.getElementsByClassName("d-dialog-box")[0];
      let bodyTextNoticeDialog = noticeDialog.querySelector(".d-dialog-body");
      bodyTextNoticeDialog.innerHTML = "Chức năng đang phát triển !";
      // hiển thị form notice
      document.getElementsByClassName("d-dialog-box")[0].style.display =
        "block";
      //ẩn form dialogDelete
      document.getElementsByClassName("dropdown")[0].style.display = "none";
    },

    /**
   * Thực hiện hiển thị form thông báo
   * Author: HVDUNG (05/06/2022)
   */
    btnOnStopUsing() {
      let noticeDialog = document.getElementsByClassName("d-dialog-box")[0];
      let bodyTextNoticeDialog = noticeDialog.querySelector(".d-dialog-body");
      bodyTextNoticeDialog.innerHTML = "Chức năng đang phát triển !";
      // hiển thị form notice
      document.getElementsByClassName("d-dialog-box")[0].style.display =
        "block";
      //ẩn form dialogDelete
      document.getElementsByClassName("dropdown")[0].style.display = "none";
    }
  },
};
</script>

<style>
@import url("../../style/css/components/dropdown.css");
</style>