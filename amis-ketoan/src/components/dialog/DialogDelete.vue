<template>
  <div class="d-dialog-box-delete">
    <div class="d-dialog">
      <div class="d-dialog-header"></div>
      <div class="d-dialog-content-box">
        <div class="d-dialog-icon-warning">
          <div class="icon_warning"></div>
        </div>
        <div class="d-dialog-body">content</div>
      </div>
      <div class="d-dialog-footer">
        <div class="d-dialog-footer-line"></div>
        <button class="d-btn" id="d-save" @click="btnCloseOnClick">Không</button>
        <button class="d-btn" id="d-save" @click="btnSaveOnClick">Có</button>
      </div>
    </div>
  </div>
</template>

<script>
import axios from "axios";
export default {
  props: ["employeeIdForDelete", "pageNumberSelected", "pageSize", "textSearch"],
  name: "DialogDelete",
  methods: {
    /**
     * Thực hiện xóa dữ liệu
     * Author: HVDUNG (05/06/2022)
     */
    async btnSaveOnClick() {
      var me = this;
      await axios
        .delete(
          `http://localhost:22454/api/v1/Employees/${me.employeeIdForDelete}`
        )
        .then(function (res) {
          console.log(res);
        })
        .catch(function (res) {
          console.log(res);
        });

      // thực hiện loadding lại dữ liệu
      try {
        //hiện loading
        document.getElementsByClassName("loading")[0].style.display = "block";
        await axios
          .get(`http://localhost:22454/api/v1/Employees/filter?pageSize=${me.pageSize}&pageNumber=${me.pageNumberSelected}&employeeFilter=${me.textSearch}`)
          .then((response) => {
            me.employees = response.data.Data;
            me.$emit("changeValueEmployees", me.employees);
            me.$emit("changeCount", response.data.TotalRecords);

          })
          .catch(function (error) {
            console.log(error);
          });
      } catch (error) {
        console.log(error);
      }
      //ẩn form dialogDelete
      document.getElementsByClassName("d-dialog-box-delete")[0].style.display = "none";
      //ẩn loading
      document.getElementsByClassName("loading")[0].style.display = "none";
    },

    /**
     * Thực hiện đóng form xóa
     * Author: HVDUNG (25/06/2022)
     */
    btnCloseOnClick() {
      document.getElementsByClassName("d-dialog-box-delete")[0].style.display = "none";
    },
  },
};
</script>

<style scoped>
@import url("../../style/css/icon/icon.css");

.d-dialog-box-delete {
  display: none;
  position: fixed;
  top: 0;
  left: 0;
  bottom: 0;
  right: 0;
  background-color: rgba(177, 177, 177, 0.397);
  z-index: 100;
}

.d-dialog-body {
  margin-top: 20px;
}
</style>>