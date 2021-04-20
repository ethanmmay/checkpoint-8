/* eslint-disable no-undef */
import { AppState } from '../AppState'
import { Keep } from '../models/Keep'
import router from '../router'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class KeepService {
  async getKeeps() {
    try {
      const res = await api.get('api/keeps')
      AppState.keeps = res.data.map(k => new Keep(k))
    } catch (err) {
      logger.error('Couldnt load Keeps', err)
    }
  }

  setActiveKeep(keep) {
    try {
      AppState.activeKeep = keep
    } catch (error) {
      logger.error('Cant set active keep', error)
    }
  }

  async createKeep() {
    try {
      Swal.fire({
        title: 'New Keep',
        html: '<input type="text" id="title" class="swal2-input" placeholder="Enter Keep Name.. "><textarea type="text" id="body" class="swal2-input pt-2" placeholder="Describe the keep..."></textarea>',
        confirmButtonText: 'Report',
        focusConfirm: false,
        preConfirm: () => {
          const title = Swal.getPopup().querySelector('#title').value
          const body = Swal.getPopup().querySelector('#body').value
          if (!title || !body) {
            Swal.showValidationMessage('Please enter title and body')
          }
          return { title: title, body: body }
        }
      }).then(async(result) => {
        const newKeep = {
          title: result.value.title,
          description: result.value.body,
          creatorId: AppState.user.id
        }
        await api.post('api/keeps', newKeep)
        AppState.keeps = []
        await this.getKeeps()
        router.push({ name: 'Keep', params: { id: AppState.keeps[AppState.keeps.length - 1].id } })
      })
    } catch (err) {
      logger.error('Couldnt create Keep', err)
    }
  }

  async deleteKeep(keepId) {
    try {
      if (window.confirm('Are you sure you want to delete your Keep?')) {
        await api.delete('api/keeps/' + keepId)
        this.getKeeps()
        document.getElementById('keepModalClose').click()
      }
    } catch (error) {
      logger.log(error)
    }
  }

  sortKeeps() {
    AppState.keeps.sort(function(a, b) { return a.closed - b.closed })
  }

  async editKeep(keep) {
    try {
      Swal.fire({
        title: 'Edit Keep',
        html: `<input type="text" id="title" class="swal2-input" placeholder="Enter Keep Name.. " value="${keep.title}"><textarea type="text" id="body" class="swal2-input pt-2" placeholder="Describe the keep...">${keep.description}</textarea>`,
        confirmButtonText: 'Save',
        focusConfirm: false,
        preConfirm: () => {
          const title = Swal.getPopup().querySelector('#title').value
          const body = Swal.getPopup().querySelector('#body').value
          if (!title || !body) {
            Swal.showValidationMessage('Please enter title and body')
          }
          return { title: title, body: body }
        }
      }).then(async(result) => {
        const newKeep = {
          title: result.value.title,
          description: result.value.body
        }
        await api.put('api/keeps/' + keep.id, newKeep)
        AppState.keeps = []
        await this.getKeeps()
      })
    } catch (err) {
      logger.error('Couldnt edit Keep', err)
    }
  }
}

export const keepService = new KeepService()
