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
    <SearchView class="column is-three-quarters" />
  </section>
</body>
</template>

<script>
import SearchView from "./views/search/SearchView";

export default {
  name: "app",
  components: {
    SearchView
  }
};
</script>

<style lang="scss">
@import "./assets/style/variables.scss";


@import "~bulma";
@import "~buefy/src/scss/buefy";
</style>
