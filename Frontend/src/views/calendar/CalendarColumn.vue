<template>
  <div class="calendar__column is-rounded">
    <p>
      <slot name="date"></slot>.
    </p>
    <p class="price">900</p>
  </div>
</template>
<script>
export default {};
</script>
<script>
export default {
  created: function() {
    // TODO: Change css if slot date is 0
  }
};
</script>
<style scoped lang="scss">
@import "../../assets/style/variables.scss";
.calendar__column {
  background-color: $light;
  border-radius: $radius-large;
  height: 100%;
}
</style>