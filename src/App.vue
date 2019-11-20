<template>
<body>
  <section class="hero is-primary is-bold">
    <div class="hero-body">
      <div class="container">
        <h1 class="title">Lowfare train</h1>
      </div>
    </div>
  </section>
  <section class="columns is-centered">
    <router-view class="column is-three-quarters"></router-view>
  </section>
</body>
</template>

<script>
export default {
  name: "app"
};
</script>
<style lang="scss">
@import "./assets/style/variables.scss";
@import "~bulma";
@import "~buefy/src/scss/buefy";
</style>
