<template>
<div>
  <TheHeader/>
  <TheNavbar/>
  <TheContent/>
</div>
</template>

<script>
import TheHeader from './components/TheHeader.vue'
import TheNavbar from './components/TheNavbar.vue'
import TheContent from './components/TheContent.vue'

export default {
  name: 'App',
  components: { 
    TheHeader,
    TheContent,
    TheNavbar
}
}
</script>

<style>
@import url(./style/css/main.css);
</style>
