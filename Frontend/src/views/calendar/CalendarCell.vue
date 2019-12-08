<template>
  <div class="calendar__cell is-rounded">
    <p id="date" class="has-text-weight-light is-family-monospace">
      <slot name="date"></slot>.
    </p>
    <p id="price" class="has-text-weight-bold">
      <slot name="price"></slot>
    </p>
  </div>
</template>
<script>
export default {};
</script>
<script>
export default {};
</script>
<style scoped lang="scss">
@import "../../assets/style/variables.scss";
.calendar__cell {
  background-color: $light;
  border-radius: $radius-large;
  height: 100%;
  padding: 4% 10% 0% 10%;
  position: relative;
}
#date {
  text-align: right;
}
#price {
  position: absolute;
  bottom: 12%;
  left: 6%;
}
.cheapest {
  #price {
    color: red;
  }
}
</style>