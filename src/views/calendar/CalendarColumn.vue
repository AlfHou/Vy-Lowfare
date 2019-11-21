<template>
    <div class="calendar__column is-rounded">
        <p><slot name="date"></slot></p>
        <p class="price">900</p>
    </div>
</template>
<script>
export default {
    
}
</script>
<script>
export default {
    
}
</script>
<style scoped lang="scss">
    @import "../../assets/style/variables.scss";
    .calendar__column{
        background-color: $light;
        margin: 2%;
        padding: 2%;
        border-radius: $radius-large;
        height: 90%;
    }
</style>