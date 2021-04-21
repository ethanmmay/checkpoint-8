export class Vault {
  constructor(data = {}) {
    this.id = data.id || ''
    this.name = data.name || ''
    this.isPrivate = data.isPrivate
    this.description = data.description || ''
    this.img = data.img || '//placehold.it/400x400'
    this.creator = data.creator || { name: 'unknown', picture: '//placehold.it/20x20' }
  }
}
