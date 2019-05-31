<template>
  <div class="login-container">
     <van-field v-model="value" placeholder="请输入用户名" />
  </div>
</template>
<script>
export default {
  data(){
    return {
      value:"",
    }
  }
}
</script>
