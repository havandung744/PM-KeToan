<template>
  <div class="d-employee-box">
    <div class="d-employee-box-info">
      <div class="header">
        <div class="header-text">THÔNG TIN NHÂN VIÊN</div>
        <div class="header-content">
          <div class="header-checkbox">
            <input type="checkbox" style="width: 18px; height: 18px" />
            <div class="header-checkbox-content">Là khách hàng</div>
          </div>
          <div class="header-checkbox">
            <input type="checkbox" style="width: 18px; height: 18px" />
            <div class="header-checkbox-content">Là nhà cung cấp</div>
          </div>
        </div>
        <div class="icon_close header-close" @click="btnCloseOnClick"></div>
      </div>
      <div class="content">
        <div class="content-info">
          <div class="content-info-left">
            <div class="info-input-tr">
              <div class="info-input">
                <label for="">Mã nhân viên&nbsp;<span style="color: red">*</span></label><br />
                <input type="text" v-model="employee.EmployeeCode" class="d-input" id="EmployeeCode"
                  @blur="validateCheckInput" tabindex="1" style="width: 150px" />
              </div>
              <div class="info-input">
                <label for="">Họ tên&nbsp;<span style="color: red">*</span></label><br />
                <input type="text" v-model="employee.EmployeeName" class="d-input" id="EmployeeName"
                  @blur="validateCheckInput" tabindex="2" style="width: 235px" />
              </div>
            </div>
            <div class="info-input-tr">
              <div class="info-input">
                <label for="">Đơn vị</label><br />

                <select tabindex="7" v-model="employee.DepartmentId" class="cbxDepartment">
                  <option value="142cb08f-7c31-21fa-8e90-67245e8b283e">
                    Phòng đào tạo
                  </option>
                  <option value="17120d02-6ab5-3e43-18cb-66948daf6128">
                    Phòng nhân sự
                  </option>
                  <option value="469b3ece-744a-45d5-957d-e8c757976496">
                    Phòng sản xuất
                  </option>
                  <option value="4e272fc4-7875-78d6-7d32-6a1673ffca7c">
                    Phòng kế toán
                  </option>
                </select>
              </div>
            </div>
            <div class="info-input-tr">
              <div class="info-input">
                <label for="">Chức danh</label><br />
                <input type="input" class="d-input" v-model="employee.EmployeePosition" tabindex="10"
                  style="width: 391px" />
              </div>
            </div>
          </div>
          <div class="content-info-right">
            <div class="info-input-tr">
              <div class="info-input">
                <label for="">Ngày sinh</label><br />
                <input type="date" id="date_timepicker_end" v-model="employee.DateOfBirth" class="d-input" tabindex="3"
                  style="width: 161px" />
                <!-- <datepicker v-model="dateTime" class="d-input" tabindex="3" inputFormat="dd/MM/yyyy"
                startingView="'year' | 'month' | 'day'"
                /> -->
              </div>
              <div class="info-input" id="d-radio">
                <label for="" style="margin-left: 4px">Giới tính</label><br />
                <div class="info-input-box">
                  <div class="info-input-item">
                    <input name="gender" type="radio" value="1" tabindex="4" v-model="employee.Gender" />
                    <label for="" style="margin-right: 8px">Nam</label>
                  </div>
                  <div class="info-input-item">
                    <input name="gender" type="radio" value="0" tabindex="5" v-model="employee.Gender" />
                    <label for="" style="margin-right: 8px">Nữ</label>
                  </div>
                  <div class="info-input-item">
                    <input name="gender" type="radio" value="2" tabindex="6" v-model="employee.Gender" />
                    <label for="">Khác</label>
                  </div>
                </div>
              </div>
            </div>
            <div class="info-input-tr">
              <div class="info-input">
                <label for="">Số CMND</label><br />
                <input type="input" v-model="employee.IdentityNumber" class="d-input" tabindex="8"
                  style="width: 245px" />
              </div>
              <div class="info-input">
                <label for="">Ngày cấp</label><br />
                <input type="date" id="date_time_IdentityDate" v-model="employee.IdentityDate" class="d-input" tabindex="9" style="width: 140px" />
              </div>
            </div>
            <div class="info-input-tr">
              <div class="info-input">
                <label for="">Nơi cấp</label><br />
                <input type="input" v-model="employee.IdentityPlace" class="d-input" tabindex="11"
                  style="width: 391px" />
              </div>
            </div>
          </div>
        </div>
        <div class="info-input-tr" style="margin-top: 40px">
          <div class="info-input">
            <label for="">Địa chỉ</label>
            <input type="input" class="d-input" v-model="employee.Address" tabindex="12" style="width: 812px" />
          </div>
        </div>
        <div class="content-info-bottom">
          <div class="info-input-tr">
            <div class="info-input">
              <label for="">ĐT di động</label><br />
              <input type="text" v-model="employee.PhoneNumber" class="d-input" id="EmployeeCode" tabindex="13"
                style="width: 150px" />
            </div>
            <div class="info-input">
              <label for="">ĐT cố định</label><br />
              <input type="text" class="d-input" id="EmployeeName" v-model="employee.TelephoneNumber" tabindex="14"
                style="width: 235px" />
            </div>
            <div class="info-input">
              <label for="">Email</label><br />
              <input type="text" v-model="employee.Email" class="d-input" id="EmployeeName" tabindex="15"
                @blur="validateCheckEmail" style="width: 235px" />
            </div>
          </div>

          <div class="info-input-tr">
            <div class="info-input">
              <label for="">Tài khoản ngân hàng</label><br />
              <input type="text" class="d-input" id="EmployeeCode" v-model="employee.BankAccountNumber" tabindex="16"
                style="width: 150px" />
            </div>
            <div class="info-input">
              <label for="">Tên ngân hàng</label><br />
              <input type="text" class="d-input" id="EmployeeName" v-model="employee.BankName" tabindex="17"
                style="width: 235px" />
            </div>
            <div class="info-input">
              <label for="">Chi nhánh</label><br />
              <input type="text" class="d-input" id="EmployeeName" v-model="employee.BankBranchName" tabindex="18"
                style="width: 235px" />
            </div>
          </div>
        </div>
        <div class="footer">
          <div class="footer-line"></div>
          <div class="footer-button">
            <button class="d-btn d-btn-cancel d-btn-footer-left" id="d-btn-close" tabindex="21" @blur="focusInputFirst"
              @click="btnCloseOnClick">
              Hủy
            </button>
            <button class="d-btn d-btn-save" id="d-btn-save" tabindex="20" @click="btnSaveOnClick">
              Cất
            </button>
            <button class="d-btn d-btn-save-add" id="d-btn-save-add" tabindex="19" @click="btnSaveAndAdd">
              Cất và Thêm
            </button>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>


<script>
import axios from "axios";
// import Datepicker from "../common/DatepickerList.vue";
export default {
  name: "EmployeeDetail",
  components: {
    //  Datepicker
  },
  props: ["employeeIdSelected", "formMode", "pageSize", "pageNumberSelected", "textSearch"],
  data() {
    return {
      // dateTime: new Date(2016, 0, 1),
      // dateTime: new Date(employee.date),
      employee: {},
      employees: {},
      checkCode: null
    };
  },

  watch: {
    /**
     * Thực hiện lấy dữ liệu từ cliend và trả về cho form chi tiết
     * @param {object} value là đối tượng nhân được khi click
     * Author: HVDUNG(05/06/2022)
     */
    employeeIdSelected: function (value) {
      // kiểm tra xem id có tồn tại hay không
      var me = this;
      if (value) {
        axios
          .get(
            `http://localhost:22454/api/v1/Employees/${me.employeeIdSelected}`

          )
          .then(function (res) {
            switch (res.status) {
              case 200:
                // thực hiện focus vào ô nhập liệu đầu tiên
                document.getElementById("EmployeeCode").focus();
                // gán lại data cho employee detail
                me.employee = res.data;

                //gán lại ngày sinh cho employee detail
                me.employee.DateOfBirth = me.formatDate(
                  me.employee.DateOfBirth
                );
                //gán lại ngày cấp cho employee detail
                me.employee.IdentityDate = me.formatDate(
                  me.employee.IdentityDate
                );
                break;
              case 201:
                break;

              default:
                break;
            }
          })
          .catch(function (res) {
            console.log(res);
          });

      } else {
        // lấy mã nhân viên mới
        me.getNewEmployeeCode();
      }
    },
  },

  methods: {
    /**
     * Thực hiện lưu khi người dùng click nút lưu
     * Author: HVDUNG (02/06/2022)
     */
    btnSaveOnClick() {
      var me = this;
      //1. validate dữ liệu
      var isValid = me.validate();
      //2. thực hiện thêm mới hoặc cập nhật khi validate thành công
      if (isValid) {
        //2.1 nếu formMode là 1 thì thêm mới
        if (me.formMode == 1) me.addEmployee();
        //2.2 nếu formMode != 1 thì cập nhật
        else me.updateEmployee();
        //3. thực hiện đóng form chi tiết
        me.$emit("isShowDialog");
      }
    },

    /**
  * Thực hiện lưu khi người dùng click nút lưu đồng thời reset value cho form detail
  * Author: HVDUNG (02/06/2022)
  */
    btnSaveAndAdd() {
      var me = this;
      //1. thực hiện việc validate
      var isValid = me.validate();
      //2. thực hiện thêm mới hoặc cập nhật khi validate thành công
      if (isValid) {
        //2.1 nếu formMode là 1 thì thêm mới
        if (me.formMode == 1) {
          me.addEmployee();
          me.employee = {};
        }
        //2.2 nếu formMode != 1 thì cập nhật
        else {
          me.updateEmployee();
          me.employee = {};
          me.$emit("changeValueFormMode", 1);
        }
      }
      me.getNewEmployeeCode();
    },

    /**
   * Thực hiện đóng form chi tiết khi click
   * Author: HVDUNG(05/06/2022)
   */
    btnCloseOnClick() {
      this.$emit("isShowDialog");
    },

    /**
    * thực hiện focus vào ô nhập liệu đầu tiên khi nó đến input cuối cùng
    * Author: HVDUNG (03/06/2022)
    */
    focusInputFirst() {
      document.getElementById("EmployeeCode").focus();
    },
    /**
     * Thực hiện format lại ngày tháng để có thể hiển thị được trên form detail
     * Author: HVDUNG (03/06/2022)
     */
    formatDate(dateOfBirth) {
      if (dateOfBirth) {
        // chuyển từ dạng string sang dạng Date
        dateOfBirth = new Date(dateOfBirth);
        // lấy ngày
        let date = dateOfBirth.getDate();
        // lấy tháng
        let month = dateOfBirth.getMonth() + 1;
        // lấy năm
        let year = dateOfBirth.getFullYear();
        // thêm số 0 vào trước nếu chỉ có một kí tự
        date = date < 10 ? `0${date}` : date;
        month = month < 10 ? `0${month}` : month;
        return `${year}-${month}-${date}`;
      } else {
        return "";
      }
    },

    /**
     * thực hiện validate xem dữ liệu có bị trống hay không
     * Author: HVDUNG (02/06/2022)
     */
    validateCheckInput(event) {
      //theo cách dùng thuần
      //1. lấy ra giá trị input nhập vào
      let value = event.currentTarget.value;
      //  2. kiểm tra value có rỗng hay không
      if (!value) {
        // thêm class vào trong input
        event.currentTarget.classList.add("d-input-error");
        // thêm câu cảnh báo khi hover
        event.currentTarget.setAttribute(
          "title",
          "thông tin không được để trống"
        );
      } else {
        event.currentTarget.classList.remove("d-input-error");
        event.currentTarget.setAttribute("title", "");
      }
    },

    /**
     * Thực hiện kiểm tra xem email đã đúng định dạng hay chưa
     * @param {event} event đối tượng được xây dựng sẵn trong Vue 
     * Author: HVDUNG (11/06/2022)
     */
    validateCheckEmail(event) {
      //theo cách dùng thuần
      //1. lấy ra giá trị input nhập vào
      let value = event.currentTarget.value;
      //  2. kiểm tra value có đụng định dạng hay không
      if (value.trim() != "" && !this.checkEmail(value)) {
        // thêm class vào trong input
        event.currentTarget.classList.add("d-input-error");
        // thêm câu cảnh báo khi hover
        event.currentTarget.setAttribute(
          "title",
          "Email không đúng định dạng"
        );
      } else {
        event.currentTarget.classList.remove("d-input-error");
        event.currentTarget.setAttribute("title", "");
      }
    },

    /**
     * Thực hiện kiểm tra email có đúng định dạng hay không
     * @param {string} email email cuả nhân viên 
     * Author: HVDUNG (11./06/2022)
     */
    checkEmail(email) {
      var res = /\S+@\S+\.\S+/;
      return res.test(email);
    },

    /**
     * Thực hiện lấy mã nhân viên mới
     * Author: HVDUNG (24/06/2022)
     */
    async getNewEmployeeCode() {
      try {
        var me = this;
        // nếu không có id thì gán lại employee là null
        me.employee = {};
        // gọi API lấy mã nhân viên
        await axios
          .get("http://localhost:22454/api/v1/Employees/NewEmployeeCode")
          .then(function (res) {
            me.employee.EmployeeCode = res.data;
            // thực hiện focus vào ô nhập liệu đầu tiên
            document.getElementById("EmployeeCode").focus();
          })
          .catch(function (res) {
            console.log(res);
          });
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Thực hiện kiểm tra mã nhân viên có bị trùng hay không
     * true - trùng
     * false - không trùng
     * Author: HVDUNG (24/06/2022)
     */
   checkDuplicateEmployeeCode(employeeCode) {
      try {
        var me = this;
        // gọi API lấy dữ liệu danh sách nhân viên
        axios
          .get("http://localhost:22454/api/v1/Employees")
          .then(function (res) {
            me.employees = res.data;
          })
          .catch(function (res) {
            console.log(res);
          });
        if (me.formMode == 1) {
          for (var res of me.employees) {
            if (employeeCode == res.EmployeeCode)
              return true;
          }
          return false;
        } 
        // else {
          // await axios
          //   .get(`http://localhost:22454/api/v1/Employees/${me.employeeIdSelected}`)
          //   .then(function (res) {
          //     me.checkCode = res.data.EmployeeCode;
          //   })
          //   .catch(function (res) {
          //     console.log(res);
          //   });
          //     if (employeeCode == me.checkCode)
          //       return false;
          //     else {
          //       for (var response of me.employees) {
          //         if (employeeCode == response.EmployeeCode)
          //           return true;
          //       }
          //       return false;
          //     }
        // }
          return false;

      } catch (error) {
        console.log(error);
      }
    },



    /**
     * Thực hiện validate dữ liệu
     * true: validate thành công
     * false: validate có lỗi
     * Author: HVDUNG (05/06/2022)
     */
    validate() {
      var me = this;
      //1. validate dữ liệu
      var isValid = true;
      var arrayErrors = [];
      if (!me.employee.EmployeeCode) {
        isValid = false;
        arrayErrors.push("Mã nhân viên không được phép để trống");
      }

      if (me.checkDuplicateEmployeeCode(me.employee.EmployeeCode)) {
        isValid = false;
        arrayErrors.push("Mã nhân viên đã tồn tại trong hệ thống");
      }

      if (!me.employee.EmployeeName) {
        isValid = false;
        arrayErrors.push("Tên nhân viên không được phép để trống");
      }
      if (!me.employee.DepartmentId) {
        isValid = false;
        arrayErrors.push("Phòng ban không được phép để trống");
      }
      // convert về dạng ngày tháng thì mới có thể so sánh được
      var dateCurrent = document.getElementById("date_timepicker_end").value;
      dateCurrent = new Date(dateCurrent);
      if (dateCurrent > new Date()) {
        isValid = false;
        arrayErrors.push("Ngày sinh không được lớn hơn ngày hiện tại");
      }
       // convert về dạng ngày tháng thì mới có thể so sánh được
      var dateCurrentIdentityDate = document.getElementById("date_time_IdentityDate").value;
      dateCurrentIdentityDate = new Date(dateCurrentIdentityDate);
      if (dateCurrentIdentityDate > new Date()) {
        isValid = false;
        arrayErrors.push("Ngày cấp không được lớn hơn ngày hiện tại");
      }

      if (me.employee.Email && !me.checkEmail(me.employee.Email)) {
        isValid = false;
        arrayErrors.push("Email không đúng định dạng");
      }
      if (!isValid) {
        //hiển thị thông báo validate không hợp lệ
        let noticeDialog = document.getElementsByClassName("d-dialog-box")[0];
        let bodyTextNoticeDialog = noticeDialog.querySelector(".d-dialog-body");
        bodyTextNoticeDialog.innerHTML = "";
        //append nội dung thông báo
        arrayErrors.forEach((errorMsg) => {
          var el = document.createElement("div");
          el.innerHTML = `${errorMsg}`;
          bodyTextNoticeDialog.append(el);
        });
        document.getElementsByClassName("d-dialog-box")[0].style.display =
          "block";
        return;
      }
      return isValid;
    },

    /**
     * Thực hiện thêm mới nhân viên
     * nhân viên được thêm mới
     * Author: HVDUNG (05/06/2022)
     */
    async addEmployee() {
      var me = this;
      document.getElementsByClassName("loading")[0].style.display = "block";
      // gán một positionId mặc định cho employee
      me.employee.PositionId = "25c6c36e-1668-7d10-6e09-bf1378b8dc91";
      await axios
        .post("http://localhost:22454/api/v1/Employees", me.employee)
        .then(function (res) {
          console.log(res);
          // dùng tạm thời => phải dùng toast msg
        })
        .catch(function (res) {
          let error = res.response.status;
          switch (error) {
            case 400:
            case 500:
              // hiển thị thông báo lỗi khi thêm mới
              console.log(res.response.data);
              alert(res.response.data["userMsg"]);
              break;

            default:
              break;
          }
        });

      // thực hiện cập nhật lại dữ liệu lên giao diện
      try {
        // hiển thị loadding
        document.getElementsByClassName("loading")[0].style.display = "block";
        console.log(me.pageNumberSelected);
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
      //ẩn loading
      document.getElementsByClassName("loading")[0].style.display = "none";

    },

    /**
     * Thực hiện cập nhật thông tin nhân viên
     * thông tin nhân viên được cập nhật
     * Author: HVDUNG (05/06/2022)
     */
    async updateEmployee() {
      var me = this;
      // 3.2 thực hiện update nhân viên
      document.getElementsByClassName("loading")[0].style.display = "block";
      await axios
        .put(
          `http://localhost:22454/api/v1/Employees/${me.employeeIdSelected}`,
          me.employee
        )
        .then(function (res) {
          console.log(res);
        })
        .catch(function (res) {
          console.log(res);
          alert(res.response.data.Data.EmployeeCode);
        });
      // thực hiện cập nhật lại dữ liệu lên giao diện
      try {
        // hiển thị loadding
        document.getElementsByClassName("loading")[0].style.display = "block";
        console.log(me.pageNumberSelected);
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
      //ẩn loading
      document.getElementsByClassName("loading")[0].style.display = "none";
    },
  },

};

</script>

<style scoped>
@import url(../../style/css/page/employee-info.css);
@import url(../../style/css/icon/icon.css);
</style>