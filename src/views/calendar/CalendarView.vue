<template>
<div>
  <div v-for="index in 5" :key="index" class="columns">
    <div
      v-for="index in 7"
      :key="index"
      class="column"
      style="height: 5rem; border: 2px solid black"
    >{{index}}</div>
  </div>
</div>
</template>
<script>
export default {};
</script>