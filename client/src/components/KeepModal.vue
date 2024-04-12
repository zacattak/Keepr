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


                    <RouterLink class="selectable" data-dismiss="keepModal"
                        :to="{ name: 'Profile', params: { accountId: account.id } }">
                        <img :src="activeKeep.creator.picture" :alt="activeKeep.creator.name" class="creatorImg">
                        <!-- <ul v-for="ingredient in recipeIngredients" :key="ingredient.id">
                        {{ ingredient.name }} | {{ ingredient.quantity }}
                    </ul> -->
                    </RouterLink>
                </div>
                <div class="modal-footer">


                    <form action="">
                        <div class="dropdown">
                            <button class="btn btn-secondary dropdown-toggle" type="button" data-bs-toggle="dropdown"
                                aria-expanded="false">
                                Dropdown button
                            </button>

                            <ul class="dropdown-menu">
                                <div class="form-select" aria-label="Select a Vault" required>
                                    <li v-for="vault in vaults" :key="vault.id" :value="vault.id" class="dropdown-item"
                                        @click="createVaultKeep(vault)">
                                        {{ vault.name }}
                                    </li>
                                </div>
                            </ul>
                        </div>
                    </form>

                    <!-- <button v-if="account.id" @click="createVaultKeep(activeKeep.id)" type="button"
                        class="btn btn-primary">Add to Vault</button> -->

                    <!-- <VaultListComponent /> -->
                </div>
            </div>
        </div>
    </div>
</template>


<script>
import { computed, onMounted, ref } from 'vue';
import { AppState } from '../AppState';
import { vaultsService } from '../services/VaultsService';
import Pop from '../utils/Pop';
import { Vault } from '../models/Vault';
import { Account } from '../models/Account';
import VaultListComponent from '../components/VaultListComponent.vue';
import { vaultKeepsService } from '../services/VaultKeepsService';
import { logger } from '../utils/Logger';



export default {
    // props: {
    //     vault: { type: Vault, required: true }
    // },
    props: {
        account: { type: Account },
        // vault: { type: Vault, required: true }
    },
    setup() {

        const editableVaultKeepData = ref({})
        // onMounted(getVaults)
        const activeKeep = computed(() => AppState.activeKeep)

        return {
            editableVaultKeepData,
            vaults: computed(() => AppState.accountVaults),
            account: computed(() => AppState.account),
            activeKeep,
            // activeVault,
            async createVaultKeep(vault) {
                try {
                    logger.log('vault id:', vault.id)
                    logger.log('keep Id', activeKeep.value.id)



                    const vaultKeepData = {
                        vaultId: vault.id,
                        keepId: activeKeep.value.id
                    }

                    await vaultKeepsService.createVaultKeep(vaultKeepData)
                }
                catch (error) {
                    Pop.error(error);
                }
            }


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
    },
    components: { VaultListComponent }
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