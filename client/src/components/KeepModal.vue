<template>
    <div class="modal fade" id="keepModal" tabindex="-1" role="dialog" aria-labelledby="keepModal" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div v-if="activeKeep" class="modal-content">
                <!-- <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel">Modal title</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div> -->
                <div class="modal-body">
                    <img :src="activeKeep.img" :alt="activeKeep.name" class="img">
                    <h2 class="text-center">{{ activeKeep.name }}</h2>
                    <p>{{ activeKeep.description }}</p>

                    <p>Views:{{ activeKeep.views }} Kept:{{ activeKeep.kept }} </p>


                    <RouterLink class="selectable" data-dismiss="keepModal" :to="{ name: 'Profile' }">
                        <img :src="activeKeep.creator.picture" :alt="activeKeep.creator.name" class="creatorImg">
                        <!-- <ul v-for="ingredient in recipeIngredients" :key="ingredient.id">
                        {{ ingredient.name }} | {{ ingredient.quantity }}
                    </ul> -->
                    </RouterLink>
                </div>
                <div class="modal-footer">

                    <!-- <div class="dropdown">
                        <button @click="getVaults()" class="btn btn-secondary dropdown-toggle" type="button"
                            id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                            vaults
                        </button>
                        <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
                            <a v-for="vault in vaults" :key="vault.id" class="dropdown-item" href="#">{{ vault.name
                                }}</a>
                        </div>
                    </div> -->

                    <!-- <button v-if="account.id" @click="createVaultKeep(activeKeep.id)" type="button"
                        class="btn btn-primary">Add to Vault</button> -->
                </div>
            </div>
        </div>
    </div>
</template>


<script>
import { computed, onMounted } from 'vue';
import { AppState } from '../AppState';
import { vaultsService } from '../services/VaultsService';
import Pop from '../utils/Pop';
import { Vault } from '../models/Vault';


export default {
    // props: {
    //     vault: { type: Vault, required: true }
    // },
    setup() {


        // onMounted(getVaults)
        const activeKeep = computed(() => AppState.activeKeep)
        return {
            vaults: computed(() => AppState.vaults),
            account: computed(() => AppState.account),
            activeKeep,



            // getVaults() {
            //     try {
            //         vaultsService.getVaults()
            //     }
            //     catch (error) {
            //         Pop.error(error);
            //     }
            // }









            // async createVaultKeep()
        }
    }
}
</script>


<style lang="scss" scoped>
.img {
    width: 100%;
    object-fit: cover;
    height: 40vh;
}

.creatorImg {
    width: 50%;
    border-radius: 100px;
    object-fit: cover;
    height: 20vh;
}
</style>